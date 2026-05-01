using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ClaimService.API.Mongo
{
    public class ClaimDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int ClaimId { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; } = "application/octet-stream";

        public byte[] Content { get; set; }
    }
}