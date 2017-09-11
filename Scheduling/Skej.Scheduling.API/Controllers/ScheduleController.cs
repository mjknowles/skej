using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Skej.Scheduling.API.Model;
using Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skej.Scheduling.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly Schedule _schedule;

        public ScheduleController()
        {

        }

        // POST /value
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ScheduleEventCommand value)
        {
            _schedule.Add(new ScheduleElement(value.Name, new DayInMonth(IsoDayOfWeek.Monday, 1)));
            return Ok(value);
        }
    }
}
