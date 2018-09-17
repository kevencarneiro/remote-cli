using System.Collections.Generic;
using System.IO;

namespace RemoteCLI.Common.Models
{
    public enum MessageType
    {
        Input,
        Output,
        Error
    }

    public class MachineInfo
    {
        public IEnumerable<SecuritySoftwareInfo> Antivirus { get; set; }
        public IEnumerable<SecuritySoftwareInfo> Firewalls { get; set; }
        public string Id { get; set; }
        public string MachineName { get; set; }
        public string NetFrameworkVersion { get; set; }
        public IEnumerable<NetworkInfo> Networks { get; set; }
        public IEnumerable<StorageDeviceInfo> StorageDevices { get; set; }
        public string WindowsVersion { get; set; }
    }

    public class NetworkInfo
    {
        public string AdapterName { get; set; }
        public string IPv4 { get; set; }
    }

    public class SecuritySoftwareInfo
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
    }

    public class StorageDeviceInfo
    {
        public long AvailableSize { get; set; }
        public DriveType DriveType { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public long TotalSize { get; set; }
    }
}