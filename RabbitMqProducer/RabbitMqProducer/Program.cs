using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMqProducer
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://prxwvhzi:E1fure87X2Ct-3xhtM_YvGuGddvziCcP@shark.rmq.cloudamqp.com/prxwvhzi");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare("test-que3", false, false, false, null);
            var message = new { Name = "Dilqem", Message = "Ivj" };
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish("","test-que3",null, body);
            Console.WriteLine($"""
                FACTORY
                {factory.HostName}
                {factory.Port}
                """);
            Console.WriteLine($"""
                CONNECTION
                {connection.Endpoint}
                {connection.LocalPort}
                {connection.Protocol}
                {connection.FrameMax}
                {connection.ClientProvidedName}
                """);
            Console.WriteLine($"""
                FACTORY
                {channel.ChannelNumber}
                {channel.CloseReason}
                """);


        }
    }
}