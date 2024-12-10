using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Data;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;

namespace ServerOfSchool.Repository
{
    public class TeacherRepository :  ITeacherRepository
    {

        private readonly DataContext _context;
        public TeacherRepository(DataContext context) 
        {
            _context = context;

        }

        public async Task AddAsync(Teacher entity)
        {

            await _context.Set<Teacher>().AddAsync(entity);
        }

        public  async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _context.Teachers
                .Include(t => t.TeacherCourses)
                .ToListAsync();
        }

        public async Task<Teacher> GetByIdAsync(int id)
        {
            return await _context.Set<Teacher>().FindAsync(id);
        }

        public async Task<Teacher> GetTeacherWithDetailsAsync(string id)
        {
            return await _context.Teachers
                .Include(t => t.TeacherCourses)
                .FirstOrDefaultAsync(t => t.ApplicationUserId == id);
        }

        public void Remove(Teacher entity)
        {
            _context.Remove(entity);
        }


        public async Task<List<Course>> GetCoursesByTeacherIdAsync(int teacherID)
        {
            var teacher = await _context.Teachers
                .Include(s => s.TeacherCourses) // Include the Courses navigation property
                .FirstOrDefaultAsync(s => s.Id == teacherID);

            if (teacher == null)
            {
                return null; // Or throw an exception if you prefer
            }

            return teacher.TeacherCourses.ToList(); // Return the list of courses associated with the student
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
