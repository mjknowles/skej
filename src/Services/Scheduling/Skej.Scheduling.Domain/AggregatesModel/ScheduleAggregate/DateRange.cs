using NodaTime;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate
{
    public class DateRange : ValueObject, IRange<LocalDateTime>
    {
        public DateRange(LocalDateTime start, LocalDateTime end)
        {
            if (start > end) throw new ArgumentException("Start of date range must be less than end.");
            Start = start;
            End = end;
        }

        public LocalDateTime Start { get; private set; }
        public LocalDateTime End { get; private set; }

        public bool Includes(LocalDateTime value)
        {
            return (Start <= value) && (value <= End);
        }

        public bool Includes(IRange<LocalDateTime> range)
        {
            return (Start <= range.Start) && (range.End <= End);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
