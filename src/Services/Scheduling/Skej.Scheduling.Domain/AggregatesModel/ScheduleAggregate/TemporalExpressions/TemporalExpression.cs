using NodaTime;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions
{
    public abstract class TemporalExpression : ValueObject
    {
        public TemporalExpressionType TemporalExpressionType { get; }

        public abstract bool Includes(LocalDateTime aDate);

        public TemporalExpression(TemporalExpressionType temporalExpressionType)
        {
            TemporalExpressionType = temporalExpressionType;
        }
    }
}
