using MediatR;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Application
{
    public class CreateScheduleElementCommandHandler : IdentifierCommandHandler<CreateScheduleElementCommand, bool>
    {
        public CreateScheduleElementCommandHandler(IMediator mediator, IRequestManager requestManager) 
            : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }
}
