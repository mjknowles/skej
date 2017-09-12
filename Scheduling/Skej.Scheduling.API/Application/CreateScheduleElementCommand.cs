using MediatR;
using Skej.Scheduling.Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Skej.Scheduling.API.Application
{
    [DataContract]
    public class CreateScheduleElementCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; }

        public CreateScheduleElementCommand(string name)
        {
            Name = name;
        }
    }
}
