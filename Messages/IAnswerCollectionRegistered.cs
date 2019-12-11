using System;
using System.Collections.Generic;

namespace Messages
{
    public interface IAnswerCollectionRegistered
    {
        string Id { get; set; }
        Guid CorrelationId { get; set; }
        IEnumerable<IAnswer> Answers { get; set; }

    }
}