using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class MandalController : Controller
    {

        private readonly AppDbContext _context;
        public MandalController(AppDbContext context)
        {
            
            _context = context;
        }
        public async Task<IActionResult> Mandal()
        {
            var sections = await _context.Mandal.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/Mandal.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddMandal(MandalModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Mandal.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/Mandal.cshtml", await _context.Mandal.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Mandal.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("Mandal");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/Mandal.cshtml", await _context.Mandal.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteMandal(int id)
        {
            var section = await _context.Mandal.FindAsync(id);
            if (section != null)
            {
                _context.Mandal.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("Mandal");
            
        }

    }
}
