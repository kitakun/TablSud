using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace TablSud.Core.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Convert fulled-enumerable to paged enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Source enumerable</param>
        /// <param name="page">Start elements index from page</param>
        /// <param name="size">Elements count</param>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> query, int page, int size)
        {
            return query.AsQueryable().Skip(page * size).Take(size).AsEnumerable();
        }

        public static IEnumerable<T> Page<T>(this IAsyncCursor<T> query, int page, int size)
        {
            return query.ToEnumerable().AsQueryable().Skip(page * size).Take(size).AsEnumerable();
        }
        
    }
}
