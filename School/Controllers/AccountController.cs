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
using School.Helpers;

namespace School.Controllers
{

    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        // private readonly string _apiBaseUrl = ;
        Uri BaseAddress = new Uri("http://localhost:5188/api/Account");
        private readonly UserSessionHelper _userSessionHelper;




        public AccountController(IHttpClientFactory httpClientFactory, UserSessionHelper userSessionHelper)
        {
            _httpClient = httpClientFactory.CreateClient("APIClient");
            _userSessionHelper = userSessionHelper;
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
                var apiResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
                // Now, you can access the roles like this:
                List<string> roles = apiResponse.roles;
                var token = apiResponse.Token;
                var id = apiResponse.Id;
                // If you need to check the roles
               
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = new TokenResponse()
                    {
                        Token = token,
                        roles = roles,
                        Id = id
                    };
                    
                    
                    // Save token in session
                    HttpContext.Session.SetString("JwtToken", tokenResponse.Token);
                    // Save token in session
                    HttpContext.Session.SetString("userId", tokenResponse.Id);

                    // Null check for Roles
                    if (tokenResponse.roles != null)
                    {
                        // Check roles and redirect accordingly
                        if (tokenResponse.roles.Contains("Student"))
                        {
                            // Save the 'id' in TempData to be used in the next request
                            TempData["UserId"] = tokenResponse.Id;
                            return RedirectToAction("Details", "Student");
                        }
                        else if (tokenResponse.roles.Contains("Teacher"))
                        {
                            // Save the 'id' in TempData to be used in the next request
                            TempData["UserId"] = tokenResponse.Id;
                            return RedirectToAction("Index", "Teacher");
                        }
                        else if (tokenResponse.roles.Contains("Admin"))
                        {
                            // Save the 'id' in TempData to be used in the next request
                            TempData["UserId"] = tokenResponse.Id;
                            return RedirectToAction("Index", "Admin");
                        }
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
            TempData["successMessage"] = "Logged in Successfully";
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
                

                return View(model);
            }

            try
            {
                
                // Use PostAsJsonAsync with the model directly
                var response = await _httpClient.PostAsJsonAsync("Account/register", model);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = " User Registerd successfuly";
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
        public string Id { get; set; }
        public string Token { get; set; }
        
        public List<string> roles { get; set; }
    }
}





