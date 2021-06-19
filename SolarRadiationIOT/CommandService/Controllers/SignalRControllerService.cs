using CommandService.Command;
using CommandService.Mqtt;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandService.Controllers
{
    public class SignalRControllerService : BackgroundService
    {
        private readonly ISubscriber _subscriber;

        public SignalRControllerService(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriber.Subscribe("AnalystServiceQueue");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("executing background loop");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
