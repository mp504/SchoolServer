using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServerOfSchool.Dto;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;

namespace ServerOfSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {

        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = _mapper.Map<List<CourseDto>>(_courseRepository.GetAllAsync());

            if (courses == null || !courses.Any())
            {
                return NotFound();
            }

            return Ok(courses);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _courseRepository.GetCourseWithDetailsAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }



        [HttpGet("{id}/students")]
        public async Task<ActionResult<Course>> GetCourseWithStudents(int id)
        {
            var course = await _courseRepository.GetCourseWithStudentsAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpGet("{id}/teachers")]
        public async Task<ActionResult<Course>> GetCourseWithTeachers(int id)
        {
            var course = await _courseRepository.GetCourseWithTeachersAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }


        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return a 400 response if model is invalid
            }
            var courseMap = _mapper.Map<Course>(course);
            await _courseRepository.AddAsync(courseMap);
            await _courseRepository.SaveChangesAsync();
            // Map the created Course back to CourseDto to return the result
            var courseDtoResult = _mapper.Map<CourseDto>(courseMap);

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _courseRepository.Remove(course);
            await _courseRepository.SaveChangesAsync();

            return NoContent();
        }




    }
}
