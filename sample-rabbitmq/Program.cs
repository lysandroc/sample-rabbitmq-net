using System;
using System.Text;
using RabbitMQ.Client;

namespace sample_rabbitmq
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

                    string message = "understading how rabbit works";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "Blog",
                        basicProperties:null,
                        body:body);

                    Console.WriteLine("Mensagem enviada!");
                }

                Console.ReadKey();
            }
        }
    }
}
