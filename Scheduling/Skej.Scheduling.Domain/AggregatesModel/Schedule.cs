using NodaTime;
using Skej.Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skej.Scheduling.Domain.AggregatesModel
{
    public class Schedule : Entity, IAggregateRoot
    {
        // copy from https://github.com/mlipper/runt/blob/master/lib/runt/schedule.rb
        private HashSet<ScheduleElement> _elements;

        public void Add(ScheduleElement element) => _elements.Add(element);

        public bool IsOccuring(string eventArg, LocalDateTime aDate)
        {
            foreach(var e in _elements)
            {
                if (e.IsOccuring(eventArg, aDate)) return true;
            }
            return false;
        }

        public List<ScheduleElement> GetScheduledDatesByMonth(DateRange dateRange)
        {
            var results = new List<ScheduleElement>();
            var date = dateRange.Start;
            while(date < dateRange.End)
            {
                foreach(var e in _elements)
                {
                    if (e.IsOccuring(date)) results.Add(e);
                }
                date = date.PlusMonths(1);
            }
            return results;
        }
    }

    public class ScheduleElement
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

    public abstract class TemporalExpression
    {
        public abstract bool Includes(LocalDateTime aDate);
    }

    /// <summary>
    /// Handles scenario such as "first monday of every month"
    /// </summary>
    public class DayInMonth : TemporalExpression
    {
        private int _count;
        private int _dayIndex;

        public DayInMonth(int dayIndex, int count)
        {
            _dayIndex = dayIndex;
            _count = count;
        }

        public override bool Includes(LocalDateTime aDate) =>  DayMatches(aDate) && WeekMatches(aDate);
        private bool DayMatches(LocalDateTime aDate) => aDate.Day == _dayIndex;
        private bool WeekMatches(LocalDateTime aDate) => _count > 0 ? WeekFromStartMatches(aDate) : WeekFromEndMatches(aDate);
        private bool WeekFromStartMatches(LocalDateTime aDate) => WeekInMonth(aDate.Day) == _count;
        private bool WeekFromEndMatches(LocalDateTime aDate) => WeekInMonth(DaysLeftInMonth(aDate) + 1) == Math.Abs(_count);
        private int WeekInMonth(int dayNumber) => ((dayNumber - 1) / 7) + 1;
        private int DaysLeftInMonth(LocalDateTime aDate) => DateTime.DaysInMonth(aDate.Year, aDate.Month) - aDate.Day;
    }

    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }

    public class DateRange : IRange<LocalDateTime>
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
    }

}
