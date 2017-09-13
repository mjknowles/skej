using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Domain.SeedWork
{
    public interface IRange<T>
    {
        T Start { get; }
        T End { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }
}
