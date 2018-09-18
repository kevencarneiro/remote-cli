using System.Threading.Tasks;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Common.Interfaces
{
    public interface IRemoteCLIClient
    {
        Task RequestMachineInfo();

        Task SendCommand(string sender, string command);
    }

    public interface IRemoteCLIServer
    {
        Task CommandOutput(string output, MessageType messageType, string sender);

        Task StoreMachineInfo(MachineInfo machineInfo);
    }
}