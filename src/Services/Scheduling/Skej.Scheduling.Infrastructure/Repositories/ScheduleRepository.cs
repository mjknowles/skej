using Microsoft.EntityFrameworkCore;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using Scheduling.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Infrastructure.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SchedulingContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ScheduleRepository(SchedulingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Schedule Add(Schedule schedule)
        {
            return _context.Schedules.Add(schedule).Entity;

        }

        public async Task<Schedule> GetAsync(int scheduleId)
        {
            var schedule = await _context.Schedules.FindAsync(scheduleId);
            if (schedule != null)
            {
                await _context.Entry(schedule)
                    .Collection(i => i.ScheduleElements).LoadAsync();
            }

            return schedule;
        }

        public void Update(Schedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }
    }
}
