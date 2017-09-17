using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Domain.AggregatesModel.ScheduleAggregate
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        Schedule Add(Schedule scheduleElement);

        void Update(Schedule scheduleElement);

        Task<Schedule> GetAsync(int scheduleElementId);
    }
}
