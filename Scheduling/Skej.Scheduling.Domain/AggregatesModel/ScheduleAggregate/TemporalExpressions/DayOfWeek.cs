using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions
{
    /// <summary>
    /// Handles scenario such as "first monday of every month"
    /// </summary>
    public class DayOfWeek : TemporalExpression
    {
        private IsoDayOfWeek _dayIndex;

        public DayOfWeek(IsoDayOfWeek dayIndex) : base(TemporalExpressionType.DayOfWeek)
        {
            _dayIndex = dayIndex;
        }

        public override bool Includes(LocalDateTime aDate) => DayMatches(aDate);
        private bool DayMatches(LocalDateTime aDate) => aDate.DayOfWeek == _dayIndex;

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
