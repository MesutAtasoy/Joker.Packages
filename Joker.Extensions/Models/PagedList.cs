using System;
using System.Collections.Generic;

namespace Joker.Extensions.Models
{
    [Serializable]
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> data)
        {
            Data = data;
        }
        
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int? NextPage { get; set; }
        public int? PreviousPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}