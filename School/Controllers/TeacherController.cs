using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using School.ViewModel;
using School.Models;

namespace School.Controllers
{
    public class TeacherController : Controller
    {
        private readonly HttpClient _httpClient;
        // private readonly string _apiBaseUrl = ;
        Uri BaseAddress = new Uri("http://localhost:5188/api");
        public TeacherController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;
        }
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("userId");


            var jwtToken = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");

            }
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Teacher/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                System.Diagnostics.Debug.WriteLine($" Response: {data}");

                // var apiResponse = JsonConvert.DeserializeObject<StudentVM>(data);
                var settings = new JsonSerializerSettings
                {
                    // This helps handle the $id and other serialization artifacts
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore

                };
                // Use a dynamic approach or create a matching root-level view model
                var apiResponse = JsonConvert.DeserializeObject<TeacherResponseVM>(data, settings);

                TeacherVM teacher = new TeacherVM()
                {
                    Id = apiResponse.Teacher.Id,
                    FirstName = apiResponse.Teacher.FirstName,
                    LastName = apiResponse.Teacher.LastName,
                    DateOfBirth = apiResponse.Teacher.DateOfBirth,
                    TeacherCourses = apiResponse.Courses.Values ?? new List<CourseVM>()
                };


                return View(teacher);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
