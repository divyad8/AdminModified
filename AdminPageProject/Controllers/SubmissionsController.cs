using Microsoft.AspNetCore.Mvc;

namespace AdminPageProject.Controllers
{
    public class SubmissionsController : Controller
    {
        public IActionResult Submissions()
        {
            return View();
        }
    }
}
