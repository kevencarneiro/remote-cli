using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RemoteCLI.Client.Interfaces;

namespace RemoteCLI.Client.Services
{
    public class RemoteCLIServiceLifetime : ServiceBase, IHostLifetime
    {
        private readonly TaskCompletionSource<object> _delayStart = new TaskCompletionSource<object>();
        private readonly ISignalRService _signalRService;

        public RemoteCLIServiceLifetime(IApplicationLifetime applicationLifetime, ISignalRService signalRService)
        {
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            _signalRService = signalRService;
        }

        private IApplicationLifetime ApplicationLifetime { get; }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _signalRService.StopAsync(cancellationToken);
            Stop();
            return Task.CompletedTask;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.Register(() => _delayStart.TrySetCanceled());
            ApplicationLifetime.ApplicationStopping.Register(Stop);

            new Thread(Run).Start(); // Otherwise this would block and prevent IHost.StartAsync from finishing.
            return _delayStart.Task;
        }

        // Called by base.Run when the service is ready to start.
        protected override void OnStart(string[] args)
        {
            _delayStart.TrySetResult(null);
            base.OnStart(args);
        }

        // Called by base.Stop. This may be called multiple times by service StopAsync, ApplicationStopping, and StopAsync.
        // That's OK because StopApplication uses a CancellationTokenSource and prevents any recursion.
        protected override void OnStop()
        {
            ApplicationLifetime.StopApplication();
            base.OnStop();
        }

        private void Run()
        {
            try
            {
                _signalRService.StartAsync();
            }
            catch (Exception ex)
            {
                _delayStart.TrySetException(ex);
            }
        }
    }
}