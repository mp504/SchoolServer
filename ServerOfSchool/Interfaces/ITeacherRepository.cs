using ServerOfSchool.Models;
using static ServerOfSchool.Interfaces.IRepository;

namespace ServerOfSchool.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task AddAsync(Teacher entity);
        void Remove(Teacher entity);
        Task<bool> SaveChangesAsync();
        Task<Teacher> GetTeacherWithDetailsAsync(int id);
        // Add other teacher-specific methods if needed
    }
}
