using MongoDB.Bson.Serialization.Attributes;

namespace BlogMgtApi.Models
{
    public class Address
    {
        [BsonElement("street")]
        public string Street { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("country")]
        public string Country { get; set; }
    }
}
