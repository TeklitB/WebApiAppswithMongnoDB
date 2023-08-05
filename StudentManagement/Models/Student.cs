using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace StudentManagement.Models
{
    /// <summary>
    /// Since we know that document databases don’t have a strict schema, 
    /// it is possible to have more fields in the JSON document then we really want to use in our application. 
    /// Also, it is possible that we don’t want to pick up all the fields from the database. 
    /// For this, we can use BsonIgnoreExtraElements attribute on the class itself.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("courses")]
        public string[]? Courses { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; } = String.Empty;

        [BsonElement("age")]
        public int Age { get; set; }
    }
}
