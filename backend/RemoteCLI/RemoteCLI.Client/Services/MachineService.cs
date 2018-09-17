using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using Microsoft.Win32;
using RemoteCLI.Client.Interfaces;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Client.Services
{
    public class MachineService : IMachineService
    {
        public MachineInfo GetMachineInfo()
        {
            var (antivirus, firewalls) = GetSecuritySoftwareInfo();
            return new MachineInfo
            {
                Id = GetMachineId(),
                MachineName = GetMachineName(),
                WindowsVersion = GetWindowsVersion(),
                NetFrameworkVersion = GetFrameworkVersion().ToString(3),
                Antivirus = antivirus,
                Firewalls = firewalls,
                Networks = GetNetworks(),
                StorageDevices = GetStorageInformation()
            };
        }

        private static Version GetFrameworkVersion()
        {
            using (var ndpKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
            {
                if (ndpKey == null)
                    throw new NotSupportedException(
                        @"No registry key found under 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full' to determine running framework version");
                var value = (int)(ndpKey.GetValue("Release") ?? 0);
                if (value >= 461808)
                    return new Version(4, 7, 2);
                if (value >= 461308)
                    return new Version(4, 7, 1);
                if (value >= 460798)
                    return new Version(4, 7, 0);
                if (value >= 394802)
                    return new Version(4, 6, 2);
                if (value >= 394254)
                    return new Version(4, 6, 1);
                if (value >= 393295)
                    return new Version(4, 6, 0);
                if (value >= 379893)
                    return new Version(4, 5, 2);
                if (value >= 378675)
                    return new Version(4, 5, 1);
                if (value >= 378389)
                    return new Version(4, 5, 0);

                throw new NotSupportedException($"No 4.5 or later framework version detected, framework key value: {value}");
            }
        }

        private static string GetMachineId()
                    => QueryWMI("Win32_ComputerSystemProduct", properties: "UUID").First()["UUID"];

        private static string GetMachineName() => Environment.MachineName;

        private static IEnumerable<NetworkInfo> GetNetworks()
                            => NetworkInterface.GetAllNetworkInterfaces()
            .Where(x => x.OperationalStatus == OperationalStatus.Up)
            .Select(x => new NetworkInfo
            {
                AdapterName = x.Name,
                IPv4 = x.GetIPProperties().UnicastAddresses
                        .FirstOrDefault(y => y.Address.AddressFamily == AddressFamily.InterNetwork)?.Address.ToString()
            });

        private static (IEnumerable<SecuritySoftwareInfo> antivirus, IEnumerable<SecuritySoftwareInfo> firewalls) GetSecuritySoftwareInfo()
        {
            string[] securitySoftwareDisabledCode = { "00", "01" };

            string GetStatusCode(string productState) =>
                Convert.ToInt32(productState).ToString("X2").PadLeft(6, '0').Substring(2, 2);

            IEnumerable<Dictionary<string, string>> SecuritySoftware(string className)
                => QueryWMI(className, "root\\SecurityCenter2", "displayName", "productState");

            bool SecuritySoftwareIsEnabled(string productState)
                => !securitySoftwareDisabledCode.Contains(GetStatusCode(productState));

            SecuritySoftwareInfo Transformation(Dictionary<string, string> x)
                => new SecuritySoftwareInfo
                {
                    Name = x["displayName"],
                    Enabled = SecuritySoftwareIsEnabled(x["productState"])
                };

            var antivirus = SecuritySoftware("AntiVirusProduct").Select(Transformation).ToArray();
            var firewalls = SecuritySoftware("FirewallProduct").Select(Transformation).ToArray();
            // todo firewalls.Append(new SecuritySoftwareInfo { Enabled = GetWindowsFirewallState(), Name = "Windows Firewall" });
            return (antivirus, firewalls);
        }

        private static IEnumerable<StorageDeviceInfo> GetStorageInformation()
                    => DriveInfo.GetDrives().ToList()
            .Where(x => x.IsReady)
            .Select(x => new StorageDeviceInfo
            {
                Name = x.Name,
                Label = x.VolumeLabel,
                AvailableSize = x.AvailableFreeSpace,
                TotalSize = x.TotalSize,
                DriveType = x.DriveType
            });

        private static IEnumerable<Dictionary<string, string>> QueryWMI(string className, string nameSpace = "root\\cimv2", params string[] properties)
        {
            var query = $"SELECT {string.Join(',', properties)} FROM {className}";
            var results = new ManagementObjectSearcher(nameSpace, query).Get();

            var result = new List<Dictionary<string, string>>();

            foreach (var item in results)
            {
                var itemProperties = properties.Select(x => new KeyValuePair<string, string>(x, item.Properties[x].Value?.ToString()));
                result.Add(new Dictionary<string, string>(itemProperties));
            }
            return result;
        }

        private string GetWindowsVersion()
                    => QueryWMI("Win32_OperatingSystem", properties: "Caption").First()["Caption"];
    }
}