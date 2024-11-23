using Microsoft.AspNetCore.Mvc;
using ServerOfSchool.Models;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Repository;
namespace SchoolServerOf.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        private readonly ICourseRepository _courseRepository;
        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        [HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _studentRepository.GetStudentWithDetailsAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            await _studentRepository.AddAsync(student);
            await _studentRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }



        // POST: api/Students/5/Courses/3
        [HttpPost("{studentId}/Courses/{courseId}")]
        public async Task<IActionResult> AddCourseToStudent(int studentId, int courseId)
        {
            var student = await _studentRepository.GetStudentWithDetailsAsync(studentId);
            if (student == null)
            {
                return NotFound($"Student with Id {studentId} not found.");
            }

            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null)
            {
                return NotFound($"Course with Id {courseId} not found.");
            }

            if (!student.Courses.Any(c => c.Id == course.Id))
            {
                student.Courses.Add(course);
                await _studentRepository.SaveChangesAsync();
            }
            return NoContent();

        }




        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _studentRepository.Remove(student);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }



        // DELETE: api/Students/5/Courses/3
        [HttpDelete("{studentId}/Courses/{courseId}")]
        public async Task<IActionResult> RemoveCourseFromStudent(int studentId, int courseId)
        {
            var student = await _studentRepository.GetStudentWithDetailsAsync(studentId);
            if (student == null)
            {
                return NotFound($"Student with Id {studentId} not found.");
            }

            var course = student.Courses.FirstOrDefault(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound($"Course with Id {courseId} not found in student's courses.");
            }

            student.Courses.Remove(course);
            await _studentRepository.SaveChangesAsync();

            return NoContent();
        }
    }

}


