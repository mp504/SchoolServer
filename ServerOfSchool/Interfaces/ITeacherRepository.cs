using ServerOfSchool.Models;


namespace ServerOfSchool.Interfaces
{
    public interface ITeacherRepository 
    {
        Task<IEnumerable<Teacher>> GetAllAsync();
        Task<Teacher> GetByIdAsync(int id);
        Task AddAsync(Teacher entity);
        void Remove(Teacher entity);
        Task<bool> SaveChangesAsync();
        Task<Teacher> GetTeacherWithDetailsAsync(string id);
        Task<List<Course>> GetCoursesByTeacherIdAsync(int teacherID);
        // Add other teacher-specific methods if needed
    }
}
