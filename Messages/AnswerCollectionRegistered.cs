using System;
using System.Collections.Generic;

namespace Messages
{
    public class AnswerCollectionRegistered : IAnswerCollectionRegistered
    {
        public AnswerCollectionRegistered(Guid correlationId, string id)
        {
            Id = id;
            CorrelationId = correlationId;
            Answers = CreateAnswers();
        }

        private IEnumerable<IAnswer> CreateAnswers()
        {
            return new List<Answer>
            {
                new Answer
                {
                    VariableName = "var1",
                    Value =   $"{Id}_value1"
                },
                new Answer
                {
                    VariableName = "var2",
                    Value = $"{Id}_value2"
                }
            };
        }

        public string Id { get; set; }
        public Guid CorrelationId { get; set; }
        public IEnumerable<IAnswer> Answers { get; set; }
    }
}