using MediatR;
using Domain.AggregatesModel.ScheduleAggregate.TemporalExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Scheduling.API.Application
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
