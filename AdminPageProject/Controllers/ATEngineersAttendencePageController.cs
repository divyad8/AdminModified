using AdminPageProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminPageProject.Controllers
{
    public class ATEngineersAttendencePageController : Controller
    {
        private readonly AppDbContext _context;

        public ATEngineersAttendencePageController(AppDbContext context)
        {
            _context = context;
        }

        // Existing method to show attendance page
        public async Task<IActionResult> ATEngineersAttendencePage()
        {
            var sections = await _context.AttendencePage.Where(a=>a.ImageColumn!=null).ToListAsync();
            return View("~/Views/Admin/ATEngineersAttendencePage.cshtml", sections);
        }

        // Method to serve images
        public IActionResult GetImage(int id)
        {
            var record = _context.AttendencePage.FirstOrDefault(a => a.Id == id);
            if (record != null && record.ImageColumn != null)
            {
                return File(record.ImageColumn, "image/jpeg"); // Adjust content type if necessary
            }
            return NotFound(); // If no image found, return 404
        }

        // New method to serve report files
        [HttpGet]
        public IActionResult GetReportFile(int id)
        {
            var record = _context.AttendencePage.FirstOrDefault(a => a.Id == id);

            if (record != null && record.Report != null)
            {
                // Assuming report files are stored as PDFs or any other document type
                string fileName = "Report_" + id + ".pdf"; // Adjust based on the file type
                string contentType = "application/pdf"; // Adjust based on the actual file type

                return File(record.Report, contentType, fileName); // Serve the file for download/view
            }

            return NotFound("Report file not found."); // Return 404 if no file is found
        }
    }
}
