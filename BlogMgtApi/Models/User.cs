using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogMgtApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("blog")]
        public string Blog { get; set; }
        [BsonElement("address")]
        public List<Address> Address { get; set; }
        [BsonElement("expertise")]
        public List<string> Expertise { get; set; }
        [BsonElement("location")]
        public List<int> Location { get; set; }
    }
}
