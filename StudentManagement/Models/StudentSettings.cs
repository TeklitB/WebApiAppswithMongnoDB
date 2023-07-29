namespace StudentManagement.Models
{
    public class StudentSettings : IStudentSettings
    {
        public string StudentCoursesCollection { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
