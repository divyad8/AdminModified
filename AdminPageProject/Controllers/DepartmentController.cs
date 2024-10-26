using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Department()
        {
            var sections = await _context.Department.ToListAsync();

            // Use Sections.cshtml in the DefiningData folder
            return View("~/Views/DefiningData/Department.cshtml", sections);
        }

        // Add new section
        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentModel section)
        {
            // Check if the provided ID already exists in the database
            var existingSection = await _context.Department.FindAsync(section.Id);
            if (existingSection != null)
            {
                ModelState.AddModelError("Id", "The section ID already exists.");
                return View("~/Views/DefiningData/Department.cshtml", await _context.Department.ToListAsync());
            }

            if (ModelState.IsValid)
            {
                _context.Department.Add(section);
                await _context.SaveChangesAsync();
                // Redirect to the Sections action to load Sections.cshtml with updated data
                return RedirectToAction("Department");
            }

            // Return the current view with the updated sections if the model state is invalid
            return View("~/Views/DefiningData/Department.cshtml", await _context.Department.ToListAsync());
        }

        // Delete a section
        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var section = await _context.Department.FindAsync(id);
            if (section != null)
            {
                _context.Department.Remove(section);
                await _context.SaveChangesAsync();
            }

            // Redirect to the Sections action to load Sections.cshtml with updated data
            return RedirectToAction("Department");
        }

    }
}
