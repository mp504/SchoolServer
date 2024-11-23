using ServerOfSchool.Models;
using static ServerOfSchool.Interfaces.IRepository;

namespace ServerOfSchool.Interfaces
{

    public interface ICourseRepository : IRepository<Course>
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course entity);
        void Remove(Course entity);
        Task<bool> SaveChangesAsync();
        Task<Course> GetCourseWithDetailsAsync(int id);
        // Add other course-specific methods if needed
    }
}
