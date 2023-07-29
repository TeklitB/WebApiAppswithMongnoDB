using Microsoft.AspNetCore.Mvc;
using SchoolMgtSystemApi.Models;
using SchoolMgtSystemApi.Services;

namespace SchoolMgtSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentsController(IStudentService service, ICourseService courseService)
        {
            _studentService = service;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetById(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            if (student.Courses.Count > 0)
            {
                var tempList = new List<Course>();
                foreach (var courseId in student.Courses)
                {
                    var course = await _courseService.GetByIdAsync(courseId);
                    if (course != null)
                        tempList.Add(course);
                }
                student.CourseList = tempList;
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _studentService.CreateAsync(student);
            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Student updatedStudent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedStudent = await _studentService.GetByIdAsync(id);
            if (queriedStudent == null)
            {
                return NotFound();
            }
            await _studentService.UpdateAsync(id, updatedStudent);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
    }
}
