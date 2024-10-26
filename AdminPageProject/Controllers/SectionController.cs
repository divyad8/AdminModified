using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AdminPageProject.Controllers
{
    public class SectionController : Controller
    {
        private readonly AppDbContext _context;

        public SectionController(AppDbContext context)
        {
            _context = context;
        }
        //<----------------------------Sections------------------------------------>

        // Fetch and display sections
        public async Task<IActionResult> Sections()
        {
            var sections = await _context.Sections.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/Sections.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddSection(SectionModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Sections.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/Sections.cshtml", await _context.Sections.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Sections.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("Sections");
            }
            
            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/Sections.cshtml", await _context.Sections.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteSection(int id)
        {
            var section = await _context.Sections.FindAsync(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("Sections");
        }
        //<----------------------------Sections------------------------------------>

        //<----------------------------Department------------------------------------>

        // Fetch and display sections
        //<----------------------------Department------------------------------------>

    }
}
