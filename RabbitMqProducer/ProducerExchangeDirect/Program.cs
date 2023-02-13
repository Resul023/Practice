using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace ProducerExchangeDirect
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://wunhsyoq:ZNKXf9YPYG0yS2nRS5A-CCVeU7UR7QUI@moose.rmq.cloudamqp.com/wunhsyoq");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare("test-direct-exc", ExchangeType.Direct);
            var message = new { Name = "Dilqem", Message = "Ivj" };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("test-direct-exc", "route-key", body: body);
        }
    }
}