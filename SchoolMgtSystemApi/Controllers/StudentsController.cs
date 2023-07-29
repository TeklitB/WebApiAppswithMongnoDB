using Microsoft.AspNetCore.Mvc;
using SchoolMgtSystemApi.Models;
using SchoolMgtSystemApi.Services;

namespace SchoolMgtSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentsController(StudentService service)
        {
            _studentService = service;
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
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            await _studentService.CreateAsync(student);
            return Ok(student);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Student updatedStudent)
        {
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
