using System;

namespace Joker.EntityFrameworkCore.Models
{
    public class PagingParams
    {
        public int CurrentPage { get; set; }
        public int PageCount  { get; set; }
        public int PageSize  { get; set; }
        public int RowCount  { get; set; }
        
        public string LinkTemplate { get; set; }

        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

}