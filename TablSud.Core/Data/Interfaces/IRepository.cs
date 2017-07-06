using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TablSud.Core.Data.Interfaces
{
    public interface IRepository<T> where T : new()
    {
        /// <summary>
        /// Get all elements
        /// </summary>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Get pageble elements from index
        /// </summary>
        IEnumerable<T> Page(int curPage = 0);

        /// <summary>
        /// Filtered querry
        /// </summary>
        IEnumerable<T> Filter(Expression<Func<T, bool>> filter);

        /// <summary>
        /// Get elements size
        /// </summary>
        int Size();
        
        /// <summary>
        /// Insert entity into db
        /// </summary>
        /// <param name="Object"></param>
        void Insert(T Object);

        /// <summary>
        /// Delete element from db by id
        /// </summary>
        void Delete(object id);
        /// <summary>
        /// Delete all elements from db
        /// </summary>
        void DeletaAll();
    }
}
