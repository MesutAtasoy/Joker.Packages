using Joker.Shared.Models.Interfaces;
using System;

namespace Joker.EntityFrameworkCore.Models
{
    public interface IBaseEntityModel : IIdentifiable<Guid> , ISoftDeletable
    {
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }

    public interface IBaseEntityModel<T> : IIdentifiable<T> , ISoftDeletable
    {
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
