using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using AnalystService.Model;
using AnalystService.Analyst;
using Newtonsoft.Json;

namespace AnalystService.Mqtt
{
    public class Subscriber
    {

        private ConnectionFactory factory;
        private DataAnalyst _da = new DataAnalyst();
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
                channel.QueueDeclare(queue: qName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                //var queueName = channel.QueueDeclare().QueueName;
                /*channel.QueueBind(queue: queueName,
                                  exchange: qName,
                                  routingKey: "");*/
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("Analyst service: Recieved [JSON message] from DataService: message=" + message);
                    SensorData sensorData = JsonConvert.DeserializeObject<SensorData>(message.ToString());
                    Console.WriteLine("Analyst service: Deserialized object from message: object=" + sensorData.UnixTime);

                    //send object for further analize
                    _da.Analyze(sensorData);

                };

                channel.BasicConsume(queue: qName,
                                     autoAck: true,
                                     consumer: consumer);
        }



    }
}
