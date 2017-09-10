using NodaTime;
using Skej.Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions
{
    public abstract class TemporalExpression : ValueObject
    {
        public abstract bool Includes(LocalDateTime aDate);
    }
}
