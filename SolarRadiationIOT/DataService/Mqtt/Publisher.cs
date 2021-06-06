using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Mqtt
{
    public class Publisher
    {
        private readonly ConnectionFactory factory;

        public Publisher()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Publish(string content, string queueName)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Fanout);

                var body = Encoding.UTF8.GetBytes(content);
                channel.BasicPublish(exchange: queueName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
