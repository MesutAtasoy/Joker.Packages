using System;
using System.Collections.Generic;
using System.Linq;
using Joker.Extensions.Models;

namespace Joker.Extensions
{
    public static class QueryableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, PaginationFilter paginationFilter)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (paginationFilter == null)
                throw new ArgumentNullException(nameof(paginationFilter));

            int skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            List<T> data = query
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToList();

            int totalRecords = query.Count();
            int totalPages = totalRecords > 0
                ? (int) Math.Ceiling((double) totalRecords / paginationFilter.PageSize)
                : 0;

            return new PagedList<T>(data)
            {
                PageNumber = paginationFilter.PageNumber,
                PageSize = paginationFilter.PageSize,
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                NextPage = paginationFilter.PageNumber < totalPages ? paginationFilter.PageNumber + 1 : default(int?),
                PreviousPage = paginationFilter.PageNumber > 1 ? paginationFilter.PageNumber - 1 : default(int?)
            };
        }
    }
}