using System;
using System.Collections.Generic;
using System.Text;

namespace Skej.Scheduling.Domain.Exceptions
{
    public class SchedulingDomainException : Exception
    {
        public SchedulingDomainException()
        { }

        public SchedulingDomainException(string message)
            : base(message)
        { }

        public SchedulingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
