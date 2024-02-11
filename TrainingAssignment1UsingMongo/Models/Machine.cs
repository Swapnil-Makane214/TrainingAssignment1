using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TrainingAssignment1UsingMongo.Models
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? machineName { get; set; }
        public List<Asset>? assetsUsed { get; set; } = new();
    }
}
