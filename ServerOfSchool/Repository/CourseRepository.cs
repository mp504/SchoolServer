using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Data;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;

namespace ServerOfSchool.Repository
{
    
        public class CourseRepository :  ICourseRepository
        {
            private readonly DataContext _context;

            public CourseRepository(DataContext context) 
            {
            _context = context;
            }

            public async Task AddAsync(Course entity)
            {
                await _context.Set<Course>().AddAsync(entity);
            }

            public  async Task<IEnumerable<Course>> GetAllAsync()
                {
                    return await _context.Courses
                        .Include(c => c.StudentCourses)
                        .Include(c => c.TeacherCourses)
                        .ToListAsync();
                }

            public async Task<Course> GetByIdAsync(int id)
            {
                return await _context.Set<Course>().FindAsync(id);
            }

            public async Task<Course> GetCourseWithDetailsAsync(int id)
                {
                    return await _context.Courses
                        .Include(c => c.StudentCourses)
                        .Include(c => c.TeacherCourses)
                        .FirstOrDefaultAsync(c => c.Id == id);
                }

            public void Remove(Course entity)
            {
        
                _context.Remove(entity);
        
            }

            public async Task<bool> SaveChangesAsync()
            {
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
}

