using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace AdminPageProject.Controllers
{
    public class VillageController : Controller
    {

        private readonly AppDbContext _context;
        public VillageController(AppDbContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Village()
        {
            var sections = await _context.Village.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/Village.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddVillage(VillageModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Village.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/Village.cshtml", await _context.Village.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Village.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("Village");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/Village.cshtml", await _context.Village.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteVillage(int id)
        {
            var section = await _context.Village.FindAsync(id);
            if (section != null)
            {
                _context.Village.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("Village");

        }

    }
}
