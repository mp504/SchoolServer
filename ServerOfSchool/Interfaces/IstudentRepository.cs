using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;


namespace ServerOfSchool.Interfaces
{
   
        public interface IStudentRepository 
        {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task AddAsync(Student T);
        void Remove(Student T);
        Task<bool> SaveChangesAsync();
        Task<Student> GetStudentWithDetailsAsync(int id);
        Task<Student> GetStudentWithDetailsByApplicationUserIdAsync(string id);
        Task<List<Course>> GetCoursesByStudentIdAsync(int studentId);
            // Add other student-specific methods if needed
        }
    
}
