using System;

namespace Messages
{
    public interface IAnswerRegistered
    {
        string Id { get; set; }
        Guid CorrelationId { get; set; }
    }
}