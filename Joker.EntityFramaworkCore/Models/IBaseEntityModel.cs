using Joker.Shared.Models.Interfaces;
using System;

namespace Joker.EntityFrameworkCore.Models
{
    public interface IBaseEntityModel : ISoftDeletable
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }

    public interface IBaseEntityModel<T> :  ISoftDeletable
    {
        T Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedOnUtc { get; set; }
        Guid CreatedBy { get; set; }
        DateTime? UpdatedOnUtc { get; set; }
        Guid? UpdatedBy { get; set; }
    }
}
