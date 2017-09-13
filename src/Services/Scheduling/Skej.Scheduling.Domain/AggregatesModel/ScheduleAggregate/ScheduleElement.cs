using NodaTime;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate
{
    public class ScheduleElement : Entity
    {
        public string Event { get; }
        public TemporalExpression Expression { get; }

        public ScheduleElement(string eventArg, TemporalExpression temporalExpression)
        {
            Event = eventArg;
            Expression = temporalExpression;
        }

        public bool IsOccuring(string eventArg, LocalDateTime aDate) => Event == eventArg ? Expression.Includes(aDate) : false;
        public bool IsOccuring(LocalDateTime aDate) => Expression.Includes(aDate);
    }
}
