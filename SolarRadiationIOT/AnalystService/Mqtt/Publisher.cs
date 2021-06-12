using AnalystService.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalystService.Mqtt
{
    public class Publisher
    {
        private readonly ConnectionFactory factory;

        public Publisher()
        {
            this.factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Publish(AnalystResult content, string queueName)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string datajson = JsonConvert.SerializeObject(content);
                var body = Encoding.UTF8.GetBytes(datajson);
                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     mandatory: true,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
