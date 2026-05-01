using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClaimService.API.Mongo
{
    public class QueryDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int QueryId { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }
    }
}
