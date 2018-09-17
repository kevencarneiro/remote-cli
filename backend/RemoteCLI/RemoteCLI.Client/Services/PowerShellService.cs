using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using RemoteCLI.Client.Interfaces;
using RemoteCLI.Common.Models;

namespace RemoteCLI.Client.Services
{
    internal class PowerShellService : IPowerShellService
    {
        private static readonly ConcurrentDictionary<string, PowerShell> Sessions = new ConcurrentDictionary<string, PowerShell>();

        public void ExecuteCommand(string sender, string command, Func<string, MessageType, string, Task> onCommandOutput)
        {
            var session = GetSession(sender);
            session.Streams.Error.DataAdded += (evt, args) =>
            {
                if (!(evt is PSDataCollection<ErrorRecord> records)) return;

                foreach (var record in records.ReadAll())
                {
                    onCommandOutput($"[ERROR] {record}", MessageType.Error, sender);
                }
            };
            session.Commands.Clear();
            session.AddScript(command);
            onCommandOutput(command, MessageType.Input, sender);
            var output = session.Invoke<string>();
            output.ToList().ForEach(async x => await onCommandOutput(x, MessageType.Output, sender));
        }

        private static PowerShell GetSession(string sender)
            => Sessions.GetOrAdd(sender, PowerShell.Create());
    }
}