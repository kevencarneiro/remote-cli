using System;
using System.Threading.Tasks;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Client.Interfaces
{
    public interface IPowerShellService
    {
        void ExecuteCommand(string sender, string command, Func<string, MessageType, string, Task> onCommandOutput);
    }
}