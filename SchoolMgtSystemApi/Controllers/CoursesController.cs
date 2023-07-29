using Microsoft.AspNetCore.Mvc;
using SchoolMgtSystemApi.Models;
using SchoolMgtSystemApi.Services;

namespace SchoolMgtSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService) 
        { 
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetById(string id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            await _courseService.CreateAsync(course);
            return Ok(course);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, Course updatedCourse)
        {
            var queriedCourse = await _courseService.GetByIdAsync(id);
            if (queriedCourse == null)
            {
                return NotFound();
            }
            await _courseService.UpdateAsync(id, updatedCourse);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            await _courseService.DeleteAsync(id);
            return NoContent();
        }
    }
}
