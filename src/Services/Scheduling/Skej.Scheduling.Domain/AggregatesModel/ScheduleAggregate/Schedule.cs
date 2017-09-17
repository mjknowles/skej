using NodaTime;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate
{
    public class Schedule : Entity, IAggregateRoot
    {
        // copy from https://github.com/mlipper/runt/blob/master/lib/runt/schedule.rb
        private readonly HashSet<ScheduleElement> _elements = new HashSet<ScheduleElement>();

        public IReadOnlyCollection<ScheduleElement> ScheduleElements => _elements;

        public string Name { get; }

        public Schedule(string name)
        {
            Name = name;
        }

        public void AddScheduleElement(ScheduleElement element) => _elements.Add(element);

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
}
