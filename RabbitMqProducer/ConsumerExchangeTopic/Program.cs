using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace ConsumerExchangeTopic;
class Program
{
    static public void Main(String[] args)
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://wunhsyoq:ZNKXf9YPYG0yS2nRS5A-CCVeU7UR7QUI@moose.rmq.cloudamqp.com/wunhsyoq");
        using IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.ExchangeDeclare("test-topic-exc", ExchangeType.Topic);


        channel.QueueDeclare("test-que3", true, false, false, null);
        //channel.QueueDeclare("test-que4", false, false, false, null);

        channel.QueueBind("test-que3", "test-topic-exc", "route.*");
        //channel.QueueBind("test-que4", "test-topic-exc", "route.*");


        EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
        consumer.Received += (sender, e) =>
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"{message}");
        };
        channel.BasicConsume("test-que3", false, consumer);
        //channel.BasicConsume("test-que4", false, consumer);
        Console.Read();
    }
}

















