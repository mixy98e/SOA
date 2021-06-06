using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AnalystService.Model;
using AnalystService.Analyst;
using Newtonsoft.Json;

namespace AnalystService.Mqtt
{
    public class Subscriber
    {

        private ConnectionFactory factory = new ConnectionFactory();
        private DataAnalyst _da = new DataAnalyst();
        public Subscriber()
        {
            factory.HostName = "rabbitmq";
            factory.Port = 5672;
            factory.UserName = "guest";
            factory.Password = "guest";
        }

        public void Subscribe(string qName)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: qName, type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: qName,
                                  routingKey: "");
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Analyst service: Recieved [JSON message] from DataService: message=" + message);
                    SensorData sensorData = JsonConvert.DeserializeObject<SensorData>(message);
                    Console.WriteLine("Analyst service: Deserialized object from message: object="+sensorData);

                    //send object for further analize
                    _da.Analyze(sensorData);

                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }



    }
}
