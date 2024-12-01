using Newtonsoft.Json;
using School.Models;

namespace School.ViewModel
{
    public class StudentResponseVM
    {
        [JsonProperty("student")]
        public StudentVM Student { get; set; }

        [JsonProperty("courses")]
        public CoursesWrapper Courses { get; set; }
    }

    public class CoursesWrapper
    {
        [JsonProperty("$values")]
        public List<CourseVM> Values { get; set; }
    }
}
