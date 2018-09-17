using System.Threading.Tasks;

namespace RemoteCLI.Client.Interfaces
{
    internal interface ISignalRService
    {
        Task Start();

        Task Stop();
    }
}