using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TablSud.Core.Domain
{
    /// <summary>
    /// Base mongo-entity implementation
    /// </summary>
    public abstract class MongoEntity
    {
        /// <summary>
        /// Mongo-db issue
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
    }
}
