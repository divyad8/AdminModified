using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace AdminPageProject.Controllers
{
    public class LocationController : Controller
    {

        private readonly AppDbContext _context;
        public LocationController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Location()
        {
            var sections = await _context.Location.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/Location.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddLocation(LocationModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Location.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/Location.cshtml", await _context.Location.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Location.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("Location");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/Location.cshtml", await _context.Location.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var section = await _context.Location.FindAsync(id);
            if (section != null)
            {
                _context.Location.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("Location");

        }

    }
}
