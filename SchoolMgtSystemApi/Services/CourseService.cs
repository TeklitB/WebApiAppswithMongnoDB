using MongoDB.Driver;
using SchoolMgtSystemApi.Models;

namespace SchoolMgtSystemApi.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courses;

        public CourseService(ISchoolDatabaseSettings settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _courses = database.GetCollection<Course>(settings.CourseCollectionName);
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courses.Find(s => true).ToListAsync();
        }

        public async Task<Course> GetByIdAsync(string id)
        {
            return await _courses.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Course> CreateAsync(Course course)
        {
            await _courses.InsertOneAsync(course);
            return course;
        }

        public async Task UpdateAsync(string id, Course course)
        {
            await _courses.ReplaceOneAsync(c => c.Id == id, course);
        }

        public async Task DeleteAsync(string id)
        {
            await _courses.DeleteOneAsync(c => c.Id == id);
        }
    }
}
