using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerOfSchool.Data;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;
using static ServerOfSchool.Interfaces.IRepository;
using static ServerOfSchool.Interfaces.IStudentRepository;

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

        
        public  async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
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

        public void Remove(Student T)
        {
            _context.Remove(T);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}


