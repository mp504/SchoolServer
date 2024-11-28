namespace ServerOfSchool.Dto
{
    public class TeacherWithCourses
    {
       public TeacherDto Teacher{ get; set; }
       public List<CourseDto> Courses { get; set; }

    }
}
