using RemoteCLI.Common.Models;

namespace RemoteCLI.Client.Interfaces
{
    internal interface IMachineService
    {
        MachineInfo GetMachineInfo();
    }
}