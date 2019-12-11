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

            Console.WriteLine("###### Lets send some messages! ######");
            Console.WriteLine("Enter number of messages you want to fire:");
            var count = Console.ReadLine();
            do
            {
                if (int.TryParse(count, out var x))
                {
                    for (var j = 0; j < x; j++)
                    {
                        SendMessage(j);
                    }
                }
                Console.WriteLine("All messages have been sent!");
                Console.WriteLine("Want more? Enter amount of messages to send, or 'quit' to exit");
                count = Console.ReadLine();
            } while (count != "quit");


            Console.ReadLine();
            _bus.StopAsync(new CancellationToken());
        }

        private static void SendMessage(in int i)
        {
            _bus.Publish(new AnswerCollectionRegistered(Guid.NewGuid(), $"id_{i}"));
        }
    }
}
