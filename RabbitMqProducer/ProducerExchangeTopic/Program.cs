using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
namespace ProducerExchangeTopic;
class Program
{
    static public void Main(String[] args)
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://wunhsyoq:ZNKXf9YPYG0yS2nRS5A-CCVeU7UR7QUI@moose.rmq.cloudamqp.com/wunhsyoq");
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();


        channel.ExchangeDeclare("test-topic-exc", ExchangeType.Topic);
        var message = new { Name = "Dilqem", Message = "Ivj Topic" };
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
        channel.BasicPublish("test-topic-exc", "route.key", body: body);

        Console.Read();
    }
}
