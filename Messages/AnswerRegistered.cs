using System;

namespace Messages
{
    public class AnswerRegistered : IAnswerRegistered
    {
        public AnswerRegistered(Guid correlationId, string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public Guid CorrelationId { get; set; }
    }
}