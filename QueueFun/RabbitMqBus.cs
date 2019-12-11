using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Messages;

namespace QueueFun
{
    public class RabbitMqBus
    {
        private static RabbitMqBus _instance;
        private IBusControl _bus;

        public static RabbitMqBus Instance()
        {
            return _instance ??= new RabbitMqBus();
        }

        public RabbitMqBus()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(ctg =>
            {
                ctg.Host(new Uri("rabbitmq://localhost"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });
            });
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _bus.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _bus.StopAsync(cancellationToken);
        }

        public void Publish(AnswerCollectionRegistered answerCollectionRegistered)
        {
            _bus.Publish<IAnswerCollectionRegistered>(answerCollectionRegistered);
        }
    }
}