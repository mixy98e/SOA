using DataService.Models;
using Newtonsoft.Json;
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
            this.factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        public void Publish(SensorData content, string queueName)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare(exchange: queueName, type: ExchangeType.Fanout);
                channel.QueueDeclare(queue: "test_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string datajson = JsonConvert.SerializeObject(content);
                var body = Encoding.UTF8.GetBytes(datajson);
                channel.BasicPublish(exchange: "",
                                     routingKey: "test_queue",
                                     mandatory: true,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
