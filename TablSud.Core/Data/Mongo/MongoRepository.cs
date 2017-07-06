using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TablSud.Core.Configuration;
using TablSud.Core.Data.Interfaces;
using TablSud.Core.Domain;

namespace TablSud.Core.Data.Mongo
{
    public class MongoRepository<T> : IRepository<T> where T : MongoEntity, new()
    {
        public const int ElementsOnPage = 50;

        #region Variables
        private readonly MongoConnectorFactory _repoFactory;
        private readonly IDbConfigurator _dbconfig;

        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        #endregion

        #region Ctor
        public MongoRepository(MongoConnectorFactory factory, IDbConfigurator dbconfig)
        {
            _repoFactory = factory ?? throw new NullReferenceException(nameof(factory));
            _dbconfig = dbconfig ?? throw new NullReferenceException(nameof(dbconfig));
        }
        #endregion

        /// <summary>
        /// Prepeare repo variables before we can use mongo methods
        /// </summary>
        private void Prepeare()
        {
            if (_client == null)
                _client = _repoFactory.GetConnection();
            if (_database == null)
                _database = _client.GetDatabase(_dbconfig.GetConfiguration().DatabaseName);
            if (_collection == null)
                _collection = _database.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> GetAll()
        {
            Prepeare();
            
            IAsyncCursor<T> result = _collection.FindSync(x => true);
            IEnumerable<T> gettedEnumerable = result.ToEnumerable();
            return gettedEnumerable;
        }

        public IEnumerable<T> Page(int curPage = 0)
        {
            Prepeare();

            var command = new
            {
                find = typeof(T).Name,
                skip = curPage * ElementsOnPage,
                limit = ElementsOnPage
            };

            BsonDocument bsonDoc = command.ToBsonDocument();
            List<object> cmdRs = _database.RunCommand<dynamic>(bsonDoc).cursor.firstBatch as List<object>;
            IEnumerable<T> parsed = cmdRs.Select(x => BsonSerializer.Deserialize<T>(x.ToJson()));
            return parsed;
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            Prepeare();

            IAsyncCursor<T> result = _collection.FindSync(filter);
            IEnumerable<T> gettedEnumerable = result.ToEnumerable();
            return gettedEnumerable;
        }

        public int Size()
        {
            Prepeare();
            JsonCommand<BsonDocument> command = new JsonCommand<BsonDocument>("{ count: '" + $"{typeof(T).Name}" + "' }");
            return _database.RunCommand(command)["n"].AsInt32;
        }
        
        public void Insert(T input)
        {
            Prepeare();

            _collection.InsertOne(input);
        }

        public void Delete(object id)
        {
            Prepeare();
            if(id is ObjectId)
                _collection.DeleteOne(x => x.Id == (ObjectId)id);
        }

        public void DeletaAll()
        {
            Prepeare();

            _collection.DeleteMany(x => true);
        }
        
    }
}
