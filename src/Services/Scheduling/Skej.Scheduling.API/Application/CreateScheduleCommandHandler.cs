using MediatR;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduling.API.Application
{
    public class CreateScheduleElementIdentifiedHandler : IdentifierCommandHandler<CreateScheduleCommand, bool>
    {
        public CreateScheduleElementIdentifiedHandler(IMediator mediator, IRequestManager requestManager)
            : base(mediator, requestManager)
        {
        }

        protected override bool CreateResultForDuplicateRequest()
        {
            return true;                // Ignore duplicate requests for creating order.
        }
    }

    public class CreateScheduleCommandHandler :
        IAsyncRequestHandler<CreateScheduleCommand, bool>

    {
        private readonly IMediator _mediator;
        private readonly IScheduleRepository _scheduleRepository;

        public CreateScheduleCommandHandler(IMediator mediator, IScheduleRepository scheduleRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _scheduleRepository = scheduleRepository;
        }

        public async Task<bool> Handle(CreateScheduleCommand message)
        {
            //TODO: add temporal expression
            var scheduleElement = new Schedule(message.Name);
            _scheduleRepository.Add(scheduleElement);

            return await _scheduleRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
