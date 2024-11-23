using Microsoft.AspNetCore.Mvc;
using SchoolServer.Models;
using SchoolServer.Interfaces;
using SchoolServer.Repository;
namespace SchoolServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IstudentRepository _studentRepsitory;
        public StudentController(IstudentRepository studentRepository)
        {
            _studentRepsitory = studentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudents()
        {
            var students = _studentRepsitory.GetStudents();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(students);
    
        }

    }
}
