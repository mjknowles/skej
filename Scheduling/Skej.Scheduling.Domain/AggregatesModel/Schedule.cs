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

        public bool IsOccuring(string eventArg, ZonedDateTime aDate)
        {
            foreach(var e in _elements)
            {
                if (e.IsOccuring(eventArg, aDate)) return true;
            }
            return false;
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

        public bool IsOccuring(string eventArg, ZonedDateTime aDate) => Event == eventArg ? Expression.Includes(aDate) : false;
        
    }
    
    public abstract class TemporalExpression
    {
        public abstract bool Includes(ZonedDateTime aDate);
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

        public override bool Includes(ZonedDateTime aDate) =>  DayMatches(aDate) && WeekMatches(aDate);
        private bool DayMatches(ZonedDateTime aDate) => aDate.Day == _dayIndex;
        private bool WeekMatches(ZonedDateTime aDate) => _count > 0 ? WeekFromStartMatches(aDate) : WeekFromEndMatches(aDate);
        private bool WeekFromStartMatches(ZonedDateTime aDate) => WeekInMonth(aDate.Day) == _count;
        private bool WeekFromEndMatches(ZonedDateTime aDate) => WeekInMonth(DaysLeftInMonth(aDate) + 1) == Math.Abs(_count);
        private int WeekInMonth(int dayNumber) => ((dayNumber - 1) / 7) + 1;
        private int DaysLeftInMonth(ZonedDateTime aDate) => DateTime.DaysInMonth(aDate.Year, aDate.Month) - aDate.Day;
    }
    
}
