using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.Models;

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

        public IActionResult Details() 
        {

            return View();
        }
    }
}
