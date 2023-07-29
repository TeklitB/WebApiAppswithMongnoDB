namespace SchoolMgtSystemApi.Models
{
    public interface ISchoolDatabaseSettings
    {
        string StudentCollectionName { get; set; }
        string CourseCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
