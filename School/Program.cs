using Microsoft.AspNetCore.Authentication.Cookies;
using School.Helpers;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddHttpClient();
// In Program.cs of your MVC project
builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5188/api/");
    // You can add default headers here if needed
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Add session services
builder.Services.AddDistributedMemoryCache(); // You need a cache provider
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; // Ensures session cookie is HttpOnly
    options.Cookie.IsEssential = true; // Makes session cookie essential for the app
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Set session timeout (optional)
});
// Register IHttpContextAccessor for use in the helper class
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Register the UserSessionHelper
builder.Services.AddScoped<UserSessionHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

// Enable session middleware
app.UseSession(); 
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
