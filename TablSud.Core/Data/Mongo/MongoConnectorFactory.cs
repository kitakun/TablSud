using System;
using MongoDB.Driver;
using TablSud.Core.Configuration;
using TablSud.Core.Data.Interfaces;

namespace TablSud.Core.Data.Mongo
{
    public class MongoConnectorFactory : IConnectorFactory<MongoClient>
    {
        private readonly IDbConfigurator _config;

        public MongoConnectorFactory(IDbConfigurator config)
        {
            _config = config ?? throw new NullReferenceException(nameof(config));
        }

        public MongoClient GetConnection()
        {
            return new MongoClient(_config.GetConfiguration().ServerUrl);
        }

        public string GetDbName => _config.GetConfiguration().DatabaseName;

    }
}
