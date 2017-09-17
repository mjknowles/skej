using MediatR;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using Scheduling.API.Application;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ScheduleController : Controller
    {
        private readonly IMediator _mediator;

        public ScheduleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // POST /value
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateScheduleCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var requestCancelOrder = new IdentifiedCommand<CreateScheduleCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestCancelOrder);
            }

            return commandResult ? (IActionResult)Ok() : (IActionResult)BadRequest();
        }
    }
}
