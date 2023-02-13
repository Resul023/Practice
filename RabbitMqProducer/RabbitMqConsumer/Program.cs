using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMqConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://prxwvhzi:E1fure87X2Ct-3xhtM_YvGuGddvziCcP@shark.rmq.cloudamqp.com/prxwvhzi");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("test-que3", true, false, false, null);

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"{sender} : {message}");
            };
            channel.BasicConsume("test-que3", false, consumer);
            Console.ReadLine();
        }
    }
}