using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SchoolMgtSystemApi.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("major")]
        public string Major { get; set; }
    }
}
