using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using School.Models;
using System.Text;
using System.Net.Http.Headers;
using NuGet.Common;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace School.Controllers
{

    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        // private readonly string _apiBaseUrl = ;
        Uri BaseAddress = new Uri("http://localhost:5188/api/Account");




        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("APIClient");
        }

        public IActionResult Login()
        {
            // Retrieve session value
            var userSession = HttpContext.Session.GetString("UserSessionKey");

            if (userSession != null)
            {
                // User is logged in or session exists
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {


            try
            {
                if (!ModelState.IsValid) return View(model);



                // Use PostAsJsonAsync with the model directly
                var response = await _httpClient.PostAsJsonAsync("Account/login", model);

                // Read the response content for more details
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();


                    // Save token in session
                    HttpContext.Session.SetString("JwtToken", tokenResponse.Token);


                    TempData["successMessage"] = "Logged in Successfully";
                    // Check roles and redirect accordingly
                    if (tokenResponse.Roles.Contains("Student"))
                    {
                        return RedirectToAction("Index", "Student");
                    }
                    else if (tokenResponse.Roles.Contains("Teacher"))
                    {
                        return RedirectToAction("Index", "Teacher");
                    }
                    else if (tokenResponse.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    // Add the error content to ModelState
                    ModelState.AddModelError(string.Empty, responseContent);

                    return View(model);
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            return View(model);


        }


        [HttpGet]
        public IActionResult Register()
        {
            var response = new RegisterModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            // First, check and log any model validation errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, ErrorMessages = x.Value.Errors.Select(e => e.ErrorMessage) })
                    .ToList();

                // Log or print out the specific validation errors
                foreach (var error in errors)
                {
                    Console.WriteLine($"Field: {error.Key}");
                    foreach (var errorMessage in error.ErrorMessages)
                    {
                        Console.WriteLine($"- {errorMessage}");
                        TempData["errorMessage"] = errorMessage;
                    }
                }

                return View(model);
            }

            try
            {
                
                // Use PostAsJsonAsync with the model directly
                var response = await _httpClient.PostAsJsonAsync("Account/register", model);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
            return View(model);

        }



    }

    // Class to match the token response
    public class TokenResponse
    {
        public string Token { get; set; }
        public string Roles {  get; set; }
    }
}





