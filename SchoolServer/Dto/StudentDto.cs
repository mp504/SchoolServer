namespace SchoolServer.Dto
{

    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public StudentAddressDTO Address { get; set; }

        //public ICollection<StudentCoursesDTO> StudentCourses { get; set; }
    }

}
