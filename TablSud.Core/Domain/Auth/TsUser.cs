using MongoDB.Bson.Serialization.Attributes;

namespace TablSud.Core.Domain.Auth
{
    public class TsUser : MongoEntity
    {
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string Login { get; set; }
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string PasswordHash { get; set; }
        [BsonElement]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public string PasswordSalt { get; set; }

        //for viewengine
        public bool IsAuthenticated
        {
            get
            {
                if (!string.IsNullOrEmpty(PasswordHash) && !string.IsNullOrEmpty(PasswordSalt))
                    return true;
                return false;
            }
        }
        public static TsUser Empty = new TsUser();
    }
}
