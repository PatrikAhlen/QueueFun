using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using QueueFun;

namespace Consumer
{
    class Program
    {
        private static RabbitMqBus _bus = RabbitMqBus.Instance();
        private static MessageCounter _messageCounter = MessageCounter.Instance();

        static void Main(string[] args)
        {
            _bus.StartAsync(new CancellationToken());
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(ep => ep.Consumer(() => new AnswerReviewedConsumer()));
            });

            bus.Start();

            var text = string.Empty;
            while (text != "quit")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("AWAITING RabbitMessages! Type quit to exit.");
                text = Console.ReadLine();
            }
        }
    }

    internal class AnswerReviewedConsumer : IConsumer<IAnswerCollectionRegistered>
    {
        public Task Consume(ConsumeContext<IAnswerCollectionRegistered> context)
        {
            var counter = MessageCounter.Instance().Inc();

            if ((counter - 1) % 10 == 0)
            {
                Console.WriteLine("");
            }
            //Console.ForegroundColor = counter % 2 == 0 ? ConsoleColor.Yellow : ConsoleColor.DarkGreen;
            Console.WriteLine($"#{counter}# :: CorrelationId: {context.Message.CorrelationId} has been recieved for Id: {context.Message.Id} => {context.Message.Answers.First().Value} | {context.Message.Answers.Last().Value}");

            return Task.CompletedTask;
        }
    }
}
