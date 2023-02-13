using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerExchangeDirect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://wunhsyoq:ZNKXf9YPYG0yS2nRS5A-CCVeU7UR7QUI@moose.rmq.cloudamqp.com/wunhsyoq");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //channel.QueueDeclare("test-que3", true, true, false, null);
            //channel.QueueDeclare("test-que4", true, true, false, null);
            //channel.QueueDeclare("test-que5", true, true, false, null);

            //channel.ExchangeDeclare("test-direct-exc", ExchangeType.Direct);
            //channel.QueueBind("test-que3", "test-direct-exc", "route-key");
            //channel.QueueBind("test-que4", "test-direct-exc", "route-key");
            //channel.QueueBind("test-que5", "test-direct-exc", "route-key");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"{message}");
            };

            channel.BasicConsume("test-que3", false, consumer);
            channel.BasicConsume("test-que4", false, consumer);
            channel.BasicConsume("test-que5", false, consumer);
            Console.Read();
        }
    }
}