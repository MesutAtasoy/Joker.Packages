//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using Joker.Objects.PagedList;
//using Microsoft.EntityFrameworkCore;

//namespace Joker.Extensions
//{
//    public static class QueryableExtensions
//    {
//        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int? page, int? pageSize) where T : class
//        {
//            page = page ?? 1;
//            pageSize = pageSize ?? 20;

//            var pagingParams = new PagingParams
//            {
//                CurrentPage = page.Value,
//                PageSize = pageSize.Value, 
//                RowCount = query.Count()
//            };
            
//            var result = new PagedResult<T>
//            {
//                Params = pagingParams
//            };
            
//            var pageCount = (double) result.Params.RowCount / pageSize.Value;
//            result.Params.PageCount = (int) Math.Ceiling(pageCount);

//            var skip = (page.Value - 1) * pageSize.Value;
//            result.Data = query.Skip(skip).Take(pageSize.Value).ToList();
//            return result;
//        }
        
//        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int? page, int? pageSize)
//            where T : class
//        {
//            page = page ?? 1;
//            pageSize = pageSize ?? 20;
            
//            var pagingParams = new PagingParams
//            {
//                CurrentPage = page.Value,
//                PageSize = pageSize.Value, 
//                RowCount = query.Count()
//            };
            
//            var result = new PagedResult<T>
//            {
//                Params = pagingParams
//            };
            
//            var pageCount = (double) result.Params.RowCount / pageSize;
//            result.Params.PageCount = (int) Math.Ceiling(pageCount.Value);

//            var skip = (page.Value - 1) * pageSize.Value;
//            result.Data = await query.Skip(skip).Take(pageSize.Value).ToListAsync();
//            return result;
//        }
//    }
//}