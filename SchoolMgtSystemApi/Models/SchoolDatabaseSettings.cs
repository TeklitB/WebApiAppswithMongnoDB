namespace SchoolMgtSystemApi.Models
{
    public class SchoolDatabaseSettings : ISchoolDatabaseSettings
    {
        public string StudentCollectionName { get; set; }
        public string CourseCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
