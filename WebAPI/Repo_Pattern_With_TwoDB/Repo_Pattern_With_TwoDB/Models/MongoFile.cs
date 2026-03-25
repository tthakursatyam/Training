using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repo_Pattern_With_TwoDB.Models
{
    public class MongoFile
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public byte[] Content { get; set; }
    }
}
