using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using School.Models;
using School.ViewModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace School.Controllers
{
    public class StudentController : Controller
    {
        private readonly HttpClient _httpClient;
        // private readonly string _apiBaseUrl = ;
        Uri BaseAddress = new Uri("http://localhost:5188/api");


        public StudentController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;
        }

        public IActionResult Index()
        {
            List<StudentVM> students = new List<StudentVM>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Student").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                students= JsonConvert.DeserializeObject<List<StudentVM>>(data);
            }
            return View(students);
        }

        public async Task<IActionResult> Details() 
        {
            var userId = HttpContext.Session.GetString("userId");
            

            var jwtToken = HttpContext.Session.GetString("JwtToken");

            if (!string.IsNullOrEmpty(jwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            }
            System.Diagnostics.Debug.WriteLine($" Response: {userId}");
            System.Diagnostics.Debug.WriteLine($" Response: {jwtToken}");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
               
            }
           
            HttpResponseMessage response = await  _httpClient.GetAsync(_httpClient.BaseAddress + $"/Student/{userId}");
            System.Diagnostics.Debug.WriteLine($"{response}");
            if (response.IsSuccessStatusCode)
            {
                var data =   response.Content.ReadAsStringAsync().Result;
                System.Diagnostics.Debug.WriteLine($" Response: {data}");

                // var apiResponse = JsonConvert.DeserializeObject<StudentVM>(data);
                var settings = new JsonSerializerSettings
                {
                    // This helps handle the $id and other serialization artifacts
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore

                };
                // Use a dynamic approach or create a matching root-level view model
                var apiResponse = JsonConvert.DeserializeObject<StudentResponseVM>(data, settings);

                System.Diagnostics.Debug.WriteLine($" Name: {apiResponse.Student.LastName + " "+apiResponse.Student.FirstName}");
                System.Diagnostics.Debug.WriteLine($" Id: {apiResponse.Student.Id}");

                System.Diagnostics.Debug.WriteLine($" Response: {data}");
                StudentVM student = new StudentVM()
                {
                    Id = apiResponse.Student.Id,
                    FirstName = apiResponse.Student.FirstName,
                    LastName = apiResponse.Student.LastName,
                    DateOfBirth = apiResponse.Student.DateOfBirth,
                    Address = apiResponse.Student.Address,
                    Courses = apiResponse.Courses.Values ?? new List<CourseVM>()
                };
               

                return View(student);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult>DeleteCourse(int studentId, int courseId)
        {
            try
            {
                var jwtToken = HttpContext.Session.GetString("JwtToken");

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }
                var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress+$"/student/{studentId}/Courses/{courseId}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Course successfully deleted.";
                    return RedirectToAction("Details");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete the course.";
                    return RedirectToAction("Details");
                }
            }
            catch (Exception ex)
            {
                
                    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                    return RedirectToAction("Details");
                
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(int studentId, int courseId)
        {
            try
            {
                var jwtToken = HttpContext.Session.GetString("JwtToken");

                if (!string.IsNullOrEmpty(jwtToken))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
                }
                // Construct the API URL with route parameters
                Uri url = new Uri(_httpClient.BaseAddress +$"/Student/{studentId}/Courses/{courseId}");
                System.Diagnostics.Debug.WriteLine($"studentid: {studentId} -courseId {courseId}");
                
                // Send POST request to add course
                var response = await _httpClient.PostAsync(_httpClient.BaseAddress + $"/Student/{studentId}/Courses/{courseId}", null);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Course successfully added.";
                    return RedirectToAction("Details");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Error: {response.StatusCode} - {errorContent}");
                    TempData["ErrorMessage"] = $"Failed to add the course. {errorContent}";
                    return RedirectToAction("Details");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Details");
            }
        }
    }



}
