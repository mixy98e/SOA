using CommandService.Mqtt;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CommandService.BackgroundServices
{
    public class SubscriberService : BackgroundService
    {
        private readonly ISubscriber subscriber;

        public SubscriberService(ISubscriber subscriber)
        {
            this.subscriber = subscriber;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            subscriber.Subscribe("AnalystServiceQueue");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("executing background loop");
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }
    }
}
