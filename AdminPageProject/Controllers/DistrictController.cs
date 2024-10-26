using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class DistrictController : Controller
    {

        private readonly AppDbContext _context;
        public DistrictController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> District()
        {
            var sections = await _context.District.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/District.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddDistrict(DistrictModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.District.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/District.cshtml", await _context.District.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.District.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("District");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/District.cshtml", await _context.District.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            var section = await _context.District.FindAsync(id);
            if (section != null)
            {
                _context.District.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("District");
        }

    }
}
