using Microsoft.AspNetCore.Mvc;
using ServerOfSchool.Models;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Repository;
using AutoMapper;
using ServerOfSchool.Dto;
using Microsoft.AspNetCore.Authorization;
namespace SchoolServerOf.Controllers
{

    //[Authorize(Roles = "Student")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        private readonly ICourseRepository _courseRepository;
        private IMapper _mapper;
        public StudentController(IStudentRepository studentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;

        }




        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentDto>))]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            // Debugging added
            var user = HttpContext.User;

            // Explicitly check role claim
            var roleClaim = user.Claims
                .FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            if (roleClaim == null || roleClaim.Value != "Student")
            {
                return Forbid(); // More specific than 404
            }
            var students =   _mapper.Map<List<StudentDto>>(_studentRepository.GetAllAsync());
            if (students == null || !students.Any())
            {
                return NotFound();
            }
            return Ok(students);
        }


        [Authorize(Roles = "Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tuple<StudentDto, List<CourseDto>>>> GetStudent(string id)
        {
            
            var student = await _studentRepository.GetStudentWithDetailsByApplicationUserIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var courses = await _studentRepository.GetCoursesByStudentIdAsync(student.Id);
            var studentDto = _mapper.Map<StudentDto>(student);
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);

            var response = new StudentWithCoursesDto
            {
                Student = studentDto,
                Courses = coursesDto
            };

            return Ok(response);
        }


        
        
        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentDto student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            var StudentMap = _mapper.Map<Student>(student);

             _studentRepository.AddAsync(StudentMap);
             _studentRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }


        // POST: api/Students/5/Courses/3
        [HttpPost("{studentId}/Courses/{courseId}")]
        [Authorize(Roles = "Student")]
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




        [Authorize(Roles = "Student")]
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


