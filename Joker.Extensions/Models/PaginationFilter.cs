namespace Joker.Extensions.Models
{
    public class PaginationFilter
    {
        private const int MaxPageSize = 100;
        private const int DefaultPageSize = 50;
        
        private int _pageNumber = 1;
        private int _pageSize = DefaultPageSize;

        public PaginationFilter() { }
        
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 0 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 0
                ? DefaultPageSize
                : value > MaxPageSize ? MaxPageSize : value;
        }
    }
}