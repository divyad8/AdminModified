using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AdminPage()
        {
            return View();
        }

        // Add methods for other actions like AllotWork, Attendance, and DefineData
       

       
        public IActionResult DefineData()
        {
            // Logic for Defining Data (to be implemented)
            return View();
        }
        public IActionResult DefiningPage()
        {
            return View();
        }
        public IActionResult ATEngineersPage()
        {
            return View();
        }
        public IActionResult location()
        {
            return View();
        }
        public IActionResult SubmitAttendencePage()
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");
            
                int userId = int.Parse(userIdString);

                // Continue with the rest of the logic for the dashboard
                ViewBag.UserId = userId;
                ViewBag.Password = password;
                return View();
        }
        public IActionResult practice()
        {
            return View();
        }

        public async Task<IActionResult> ShowWork()
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");

            int userId = int.Parse(userIdString);
            ViewBag.UserId = userId;
            ViewBag.Password = password;
            var sections = await _context.AttendencePage.Where(a => a.EngineerId == userId).ToListAsync();
            return View("~/Views/Engineers/ShowWork.cshtml", sections);
        }
        public async Task<IActionResult> Records()
        {
            string userIdString = HttpContext.Session.GetString("UserID");
            string password = HttpContext.Session.GetString("Password");

            int userId = int.Parse(userIdString);
            ViewBag.UserId = userId;
            ViewBag.Password = password;
            var sections = await _context.AttendencePage.Where(a => a.EngineerId == userId).ToListAsync();
            return View("~/Views/Engineers/Records.cshtml", sections);
        }
    }
}
