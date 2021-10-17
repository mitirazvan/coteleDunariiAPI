using CoteleDunarii.WebServices.WebScrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoteleDunarii.WebServices.HostedService
{
    public class SyncHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SyncHostedService> _logger;
        private Timer _timer;
        public IServiceProvider Services { get; }

        public SyncHostedService(ILogger<SyncHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Sync Hosted Service running.");

            // Starts the sync immediately and again after every 24 hours
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _ = DoWorkAsync();
        }

        private async Task DoWorkAsync()
        {
            var count = Interlocked.Increment(ref executionCount);

            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService = scope.ServiceProvider.GetRequiredService<IScrapper>();

                await scopedProcessingService.RetrieveData();
            }

            _logger.LogInformation("Timed Sync Hosted Service is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Sync Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            _timer?.Dispose();
        }
    }
}
