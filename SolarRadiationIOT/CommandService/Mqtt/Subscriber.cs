using CommandService.Model;
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
    public class Subscriber : ISubscriber
    {

        private ConnectionFactory factory;
        //private Command.CommandService _cs = new Command.CommandService();
        private Command.CommandService _cs;
        private IModel channel;
        private IConnection connection;


        public Subscriber(Command.CommandService cs)
        {
            _cs = cs;
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
            channel.QueueDeclare(queue: qName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Command service: Recieved [JSON message] from AnalystService: message=" + message);
                AnalystResult analystResult = JsonConvert.DeserializeObject<AnalystResult>(message.ToString());
                Console.WriteLine("Command service: Deserialized object from message: object=" + analystResult.RadiationHigh);

                //send object for further command sending
                _cs.checkCommands(analystResult);
            };

            channel.BasicConsume(queue: qName,
                                 autoAck: true,
                                 consumer: consumer);
        }

    }
}
