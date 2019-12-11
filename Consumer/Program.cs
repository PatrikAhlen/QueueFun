using System;
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

            Console.ResetColor();

        }
    }

    internal class AnswerReviewedConsumer : IConsumer<IAnswerRegistered>
    {
        public Task Consume(ConsumeContext<IAnswerRegistered> context)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"CorrelationId: {context.Message.CorrelationId} has been recieved for Id: {context.Message.Id}!");
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}
