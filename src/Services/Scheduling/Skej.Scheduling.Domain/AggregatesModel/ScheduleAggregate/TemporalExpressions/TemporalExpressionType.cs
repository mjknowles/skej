using Scheduling.Domain.Exceptions;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions
{
    public class TemporalExpressionType : Enumeration
    {
        public static TemporalExpressionType DayInMonth = new TemporalExpressionType(1, nameof(DayInMonth).ToLowerInvariant());
        public static TemporalExpressionType DayOfWeek = new TemporalExpressionType(2, nameof(DayOfWeek).ToLowerInvariant());

        protected TemporalExpressionType()
        {
        }

        public TemporalExpressionType(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<TemporalExpressionType> List() =>
            new[] { DayInMonth, DayOfWeek };

        public static TemporalExpressionType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new SchedulingDomainException($"Possible values for TemporalExpressionType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TemporalExpressionType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new SchedulingDomainException($"Possible values for TemporalExpressionType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
