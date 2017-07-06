using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain;

namespace TablSud.Core.Data.Mongo
{
    /// <summary>
    /// Presetted InCache "repository", who gets data from List<typeparamref name="T"/>
    /// </summary>
    public class EmptyMongoRepository<T> : IRepository<T> where T : MongoEntity, new()
    {
        private readonly List<T> _collection = new List<T>(3)
        {
            default(T),
            default(T),
            default(T),
        };

        public IEnumerable<T> GetAll()
        {
            return _collection.AsEnumerable();
        }

        public IEnumerable<T> Page(int curPage = 0)
        {
            return _collection.AsEnumerable();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            return _collection.AsEnumerable();
        }

        public int Size()
        {
            return _collection.Count;
        }
        
        public void Insert(T Object)
        {
            _collection.Add(Object);
        }

        public void Delete(object id)
        {
            _collection.Remove(_collection.Find(x => x.Id == (ObjectId) id));
        }

        public void DeletaAll()
        {
            _collection.Clear();
        }
    }
}
