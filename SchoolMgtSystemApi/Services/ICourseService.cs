using SchoolMgtSystemApi.Models;

namespace SchoolMgtSystemApi.Services
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(string id);
        Task<Course> CreateAsync(Course course);
        Task UpdateAsync(string id, Course course);
        Task DeleteAsync(string id);
    }
}
