using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions
{
    /// <summary>
    /// Handles scenario such as "first monday of every month"
    /// </summary>
    public class DayInMonth : TemporalExpression
    {
        private int _count;
        private IsoDayOfWeek _dayIndex;

        public DayInMonth(IsoDayOfWeek dayIndex, int count)
        {
            _dayIndex = dayIndex;
            _count = count;
        }

        public override bool Includes(LocalDateTime aDate) => DayMatches(aDate) && WeekMatches(aDate);
        private bool DayMatches(LocalDateTime aDate) => aDate.DayOfWeek == _dayIndex;
        private bool WeekMatches(LocalDateTime aDate) => _count > 0 ? WeekFromStartMatches(aDate) : WeekFromEndMatches(aDate);
        private bool WeekFromStartMatches(LocalDateTime aDate) => WeekInMonth(aDate.Day) == _count;
        private bool WeekFromEndMatches(LocalDateTime aDate) => WeekInMonth(DaysLeftInMonth(aDate) + 1) == Math.Abs(_count);
        private int WeekInMonth(int dayNumber) => ((dayNumber - 1) / 7) + 1;
        private int DaysLeftInMonth(LocalDateTime aDate) => DateTime.DaysInMonth(aDate.Year, aDate.Month) - aDate.Day;

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
