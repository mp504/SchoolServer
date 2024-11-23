using SchoolServer.Models;

namespace SchoolServer.Interfaces
{
    public interface IstudentRepository
    {
        ICollection<Student> GetStudents();
    }
}
