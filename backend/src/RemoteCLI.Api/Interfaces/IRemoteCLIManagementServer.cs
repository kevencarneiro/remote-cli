using System.Threading.Tasks;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Api.Interfaces
{
    public interface IRemoteCLIManagementClient
    {
        Task CommandOutput(string output);

        Task MachineRegistered(MachineInfo machine);

        Task MachineUnregistered(string MachineId);
    }

    public interface IRemoteCLIManagementServer
    {
        Task SendCommand(string machineId, string command);
    }
}