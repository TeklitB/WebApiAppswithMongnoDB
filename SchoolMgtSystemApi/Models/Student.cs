using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SchoolMgtSystemApi.Models
{
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("major")]
        public string Major { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Courses { get; set; }
        [BsonIgnore]
        public List<Course> CourseList { get; set; }
    }
}
