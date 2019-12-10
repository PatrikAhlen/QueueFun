using System;
using System.Threading;
using Messages;

namespace QueueFun
{
    class Program
    {
        private static RabbitMqBus _bus = RabbitMqBus.Instance();

        static void Main(string[] args)
        {
            _bus.StartAsync(new CancellationToken());

            Console.WriteLine("Hello World!");
            Console.WriteLine("Enter number of messages you want to fire:");
            var count = Console.ReadLine();

            if (int.TryParse(count, out var x))
            {
                for (var j = 0; j < x; j++)
                {
                    SendMessage(j);
                }
            }

            Console.ReadLine();
            _bus.StopAsync(new CancellationToken());
        }

        private static void SendMessage(in int i)
        {
            _bus.Publish(new AnswerRegistered(Guid.NewGuid(), $"id_{i}"));
        }
    }
}
