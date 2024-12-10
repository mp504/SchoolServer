using Newtonsoft.Json;
using School.Models;

namespace School.ViewModel
{
    public class TeacherResponseVM
    {
        
            [JsonProperty("teacher")]
            public TeacherVM Teacher { get; set; }

            [JsonProperty("courses")]
            public CoursesWrapper Courses { get; set; }

        

      
    }
    
}
