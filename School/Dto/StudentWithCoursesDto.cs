namespace ServerOfSchool.Dto
{
    public class StudentWithCoursesDto
    {
        public StudentDto Student { get; set; }
        public List<CourseDto> Courses { get; set; }
    }
}
