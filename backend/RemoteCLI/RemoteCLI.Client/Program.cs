using System;
using Topshelf;

namespace RemoteCLI.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                x.Service<RemoteCLIAgent>(s =>
                {
                    s.ConstructUsing(name => new RemoteCLIAgent());
                    s.WhenStarted(async agent => await agent.Start());
                    s.WhenStopped(async agent => await agent.Stop());
                });

                x.SetDescription("Cliente RemoteCLI que permite que comandos PowerShell sejam executador por uma interface web");
                x.SetDisplayName("RemoteCLIAgent");
                x.SetServiceName("RemoteCLIAgent");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}