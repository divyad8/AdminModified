using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace AdminPageProject.Controllers
{
    public class ATEngineerController : Controller
    {
        private readonly AppDbContext _context;

        public ATEngineerController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> ATEngineer()
        {
            var sections = await _context.ATEngineer.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/ATEngineer.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddATEngineer(ATEngineerModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.ATEngineer.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/ATEngineer.cshtml", await _context.ATEngineer.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.ATEngineer.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("ATEngineer");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/ATEngineer.cshtml", await _context.ATEngineer.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteATEngineer(int id)
        {
            var section = await _context.ATEngineer.FindAsync(id);
            if (section != null)
            {
                _context.ATEngineer.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("ATEngineer");
        }
    }
}
