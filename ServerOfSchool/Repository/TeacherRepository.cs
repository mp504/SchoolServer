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

        public async Task<Teacher> GetTeacherWithDetailsAsync(int id)
        {
            return await _context.Teachers
                .Include(t => t.TeacherCourses)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public void Remove(Teacher entity)
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
