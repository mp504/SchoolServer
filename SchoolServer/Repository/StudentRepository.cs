using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using SchoolServer.Interfaces;
using SchoolServer.Models;

namespace SchoolServer.Repository
{
    public class StudentRepository: IstudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Student> GetStudents() 
        {
            var students =  _context.Students
            .Include(s => s.Address).Include(s => s.Courses)             // Include the related Address
                 // Include the related StudentCourses
            .ToList();

            //return _context.Students.OrderBy(p=>p.Id)..Include(s => s.Address)             // Include the related Address
            //.Include(s => s.StudentCourses).ToList();      // Include the related StudentCourses
            //                                               
            return students;
        }

    }

}
