using Microsoft.AspNetCore.Authentication.Cookies;
using School.Helpers;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5188/api/");
    
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true; 
    options.IdleTimeout = TimeSpan.FromMinutes(20); 
});

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
