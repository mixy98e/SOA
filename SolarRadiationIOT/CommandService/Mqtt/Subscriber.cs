using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandService.Mqtt
{
    public class Subscriber
    {
        private ConnectionFactory factory;
        private IModel channel;
        private IConnection connection;

        public Subscriber()
        {
            factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Subscribe(string qName)
        {
            //channel.ExchangeDeclare(exchange: qName, type: ExchangeType.Fanout);
            channel.QueueDeclare(queue: "AnalystService",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //var queueName = channel.QueueDeclare().QueueName;
            /*channel.QueueBind(queue: queueName,
                              exchange: qName,
                              routingKey: "");*/
            var consumer = new EventingBasicConsumer(channel);

            //skinuti komentar
            /*consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Analyst service: Recieved [JSON message] from DataService: message=" + message);
                SensorData sensorData = JsonConvert.DeserializeObject<SensorData>(message.ToString());
                Console.WriteLine("Analyst service: Deserialized object from message: object=" + sensorData.UnixTime);

                //send object for further analize
                _da.Analyze(sensorData);

            };*/

            channel.BasicConsume(queue: "test_queue",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
