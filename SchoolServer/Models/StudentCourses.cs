using System.Text.Json.Serialization;

namespace SchoolServer.Models
{
    // Relationship between student and class Many To Many.
    public class StudentCourses
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        [JsonIgnore]
        public int StudentId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
    }
}
