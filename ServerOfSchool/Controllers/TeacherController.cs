using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerOfSchool.Dto;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;
using System.Collections.Generic;

namespace ServerOfSchool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherRepository teacherRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAllAsync();

            var teachersDto = _mapper.Map<List<TeacherDto>>(teachers);
            return Ok(teachersDto);
        }


        [Authorize(Roles ="Teacher")]
        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tuple<TeacherDto, List<CourseDto>>>> GetTeacher(int id)
        {
            var teacher = await _teacherRepository.GetTeacherWithDetailsAsync(id);

            if (teacher == null)
            {
                return NotFound();
            }
            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            var courses = await _teacherRepository.GetCoursesByTeacherIdAsync(id);
            var coursesDto = _mapper.Map<List<CourseDto>>(courses);

            var response = new TeacherWithCourses
            {
                Teacher = teacherDto,
                Courses = coursesDto
            };
            return Ok(response);
        }


        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherDto teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            var teacherMap = _mapper.Map<Teacher>(teacher);

            await _teacherRepository.AddAsync(teacherMap);
            await _teacherRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeacher), new { id = teacher.Id }, teacher);
        }



        [Authorize(Roles = "Admin")]
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


        [Authorize(Roles = "Teacher")]
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


        [Authorize(Roles = "Teacher")]
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
