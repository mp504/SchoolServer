using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Data;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;

namespace ServerOfSchool.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Student T)
        {
            await _context.Set<Student>().AddAsync(T);
        }

        
        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await  _context.Students
                .Include(s => s.Address)
                .Include(s => s.Courses)
                .ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {

            return await _context.Set<Student>().FindAsync(id);
        }

        public async Task<Student> GetStudentWithDetailsAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Address)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Student> GetStudentWithDetailsByApplicationUserIdAsync(string id)
        {
            return await _context.Students
                .Include(s => s.Address)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.ApplicationUserId == id);
        }
        public void Remove(Student T)
        {
            _context.Remove(T);
        }



        public async Task<List<Course>> GetCoursesByStudentIdAsync(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.Courses) // Include the Courses navigation property
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return null; // Or throw an exception if you prefer
            }

            return student.Courses.ToList(); // Return the list of courses associated with the student
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}


