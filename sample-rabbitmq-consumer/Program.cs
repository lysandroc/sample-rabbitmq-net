using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace sample_rabbitmq_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var con = factory.CreateConnection())
            {
                using (var channel = con.CreateModel())
                {
                    channel.QueueDeclare(queue: "Blog",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine(message);
                    };

                    channel.BasicConsume(queue: "Blog", 
                        autoAck: true, 
                        consumer: consumer);

                    Console.WriteLine("Mensagem recebida!");
                }

                Console.ReadKey();
            }
        }
    }
}
