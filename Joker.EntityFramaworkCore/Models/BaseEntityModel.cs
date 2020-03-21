using System;

namespace Joker.EntityFrameworkCore.Models
{
    public class BaseEntityModel :  IBaseEntityModel<Guid>
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set ; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }

    public class BaseEntityModel<T> : IBaseEntityModel<T>
    {
        public T Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
