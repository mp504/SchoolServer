namespace SchoolServer.Models
{
    // Relationship between teacher and class Many To Many.
    public class TeacherCourses
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
