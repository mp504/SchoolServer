using Microsoft.AspNetCore.Mvc;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;

namespace ServerOfSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public TeacherController(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
        }


        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            return Ok(teachers);
        }



        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            var teacher = await _teacherRepository.GetTeacherWithDetailsAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }


        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(Teacher teacher)
        {
            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher);
        }



        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _teacherRepository.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _teacherRepository.Remove(teacher);
            await _teacherRepository.SaveChangesAsync();

            return NoContent();
        }


        // POST: api/Teachers/5/Courses/3
        [HttpPost("{teacherId}/Courses/{courseId}")]
        public async Task<IActionResult> AddCourseToTeacher(int teacherId, int courseId)
        {
            var teacher = await _teacherRepository.GetTeacherWithDetailsAsync(teacherId);
            if (teacher == null)
            {
                return NotFound($"Teacher with Id {teacherId} not found.");
            }

            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return NotFound($"Course with Id {courseId} not found.");
            }

            if (!teacher.TeacherCourses.Any(c => c.Id == course.Id))
            {
                teacher.TeacherCourses.Add(course);
                await _teacherRepository.SaveChangesAsync();
            }

            return NoContent();
        }


        // DELETE: api/Teachers/5/Courses/3
        [HttpDelete("{teacherId}/Courses/{courseId}")]
        public async Task<IActionResult> RemoveCourseFromTeacher(int teacherId, int courseId)
        {
            var teacher = await _teacherRepository.GetTeacherWithDetailsAsync(teacherId);
            if (teacher == null)
            {
                return NotFound($"Teacher with Id {teacherId} not found.");
            }

            var course = teacher.TeacherCourses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound($"Course with Id {courseId} not found in teacher's courses.");
            }

            teacher.TeacherCourses.Remove(course);
            await _teacherRepository.SaveChangesAsync();

            return NoContent();
        }


    }
}
