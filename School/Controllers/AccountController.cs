using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using School.Models;
using System.Text;

namespace School.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
       // private readonly string _apiBaseUrl = ;
        Uri BaseAddress = new Uri("http://localhost:5188/api/Account");




        public AccountController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = BaseAddress;
        }

        public IActionResult Login()
        {
            return View();
        }


        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                //if (!ModelState.IsValid) return View();

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json,Encoding.ASCII, "application/json");


                var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Optionally, store authentication info like JWT token in session or cookies
                    // Redirect to Home page or dashboard after successful login
                    TempData["succesMessage"] = "logedin Successfully";
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMeassage"] = ex.Message;
                return View();
            }



            return View();
            }
    }
}
