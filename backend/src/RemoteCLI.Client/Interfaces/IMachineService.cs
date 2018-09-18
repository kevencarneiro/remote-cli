using RemoteCLI.Common.Models;

namespace RemoteCLI.Client.Interfaces
{
    public interface IMachineService
    {
        MachineInfo GetMachineInfo();
    }
}