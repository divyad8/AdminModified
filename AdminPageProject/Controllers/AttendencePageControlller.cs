using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class AttendencePageController : Controller
    {
        private readonly AppDbContext _context;

        public AttendencePageController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> AttendencePage()
        {
            var sections = await _context.AttendencePage.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/Admin/AttendencePage.cshtml", sections);
        }
    }
}
