using ServerOfSchool.Models;
using static ServerOfSchool.Interfaces.IRepository;

namespace ServerOfSchool.Interfaces
{

    public interface ICourseRepository 
    {
        IEnumerable<Course> GetAllAsync();

        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course entity);
        void Remove(Course entity);
        Task<bool> SaveChangesAsync();
        Task<Course> GetCourseWithDetailsAsync(int id);

        Task<Course> GetCourseWithStudentsAsync(int id);
        Task<Course> GetCourseWithTeachersAsync(int id);
    }
}
