using MongoDB.Driver;
using SchoolMgtSystemApi.Models;

namespace SchoolMgtSystemApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentService(ISchoolDatabaseSettings settings, IMongoClient client)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentCollectionName);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _students.Find(s => true).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _students.Find(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }

        public async Task UpdateAsync(string id, Student student)
        {
            await _students.ReplaceOneAsync(s => s.Id == id, student);
        }

        public async Task DeleteAsync(string id)
        {
            await _students.DeleteOneAsync(s => s.Id == id);
        }
    }
}
