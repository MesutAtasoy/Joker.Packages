using System;
using System.Collections.Generic;
using System.Text;

namespace Joker.Shared.Models.Interfaces
{
    public interface IIdentifiable : IIdentifiable<Guid>
    {
    }

    public interface IIdentifiable<T> 
    {
        T Id { get; }
    }
}
