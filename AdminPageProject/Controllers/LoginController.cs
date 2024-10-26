using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(LoginModel model)
        {
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == model.Id && a.Password == model.Password);
            var engineer = await _context.ATEngineer
                .FirstOrDefaultAsync(a => a.Id == model.Id && a.Password == model.Password);
            if (admin != null)
            {
                return RedirectToAction("AdminPage", "Admin");
            }
            else if (engineer != null)
            {
                HttpContext.Session.SetString("UserID", engineer.Id.ToString());
                HttpContext.Session.SetString("Password", engineer.Password);
                return RedirectToAction("ATEngineersPage", "Admin");
            }
            else
            {
                ViewBag.Error = "Invalid username or password!";
                return View("Index");
            }
        }
    }
}
