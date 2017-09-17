using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Scheduling.API.Application
{
    [DataContract]
    public class CreateScheduleCommand : IRequest<bool>
    {
        [DataMember]
        public string Name { get; }

        public CreateScheduleCommand(string name)
        {
            Name = name;
        }
    }
}
