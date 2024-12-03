using Microsoft.AspNetCore.Mvc;

namespace School.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
