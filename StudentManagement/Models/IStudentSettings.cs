namespace StudentManagement.Models
{
    public interface IStudentSettings
    {
        string StudentCoursesCollection { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
