using AdminPageProject.Data;
using AdminPageProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AdminPageProject.Controllers
{
    public class AllotworkController : Controller
    {
        private readonly AppDbContext _context;

        public AllotworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Fetch and display sections
        public async Task<IActionResult> AllotWork()
        {
            var viewModel = new CombinedViewModel
            {
                Sections = await _context.Sections.ToListAsync(),
                Departments = await _context.Department.ToListAsync(),
                District = await _context.District.ToListAsync(),
                Mandal = await _context.Mandal.ToListAsync(),
                Village = await _context.Village.ToListAsync(),
                Location = await _context.Location.ToListAsync(),
                ATEngineer = await _context.ATEngineer.ToListAsync()
            };

            return View("~/Views/Admin/AllotWork.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllotWork(string postalCode, int postalNo, DateTime postalDate, IFormFile postalCopy, int atEngineerId, string section, string department, string district, string mandal, string village, string location)
        {
            // Validate required dropdowns are selected
            if (string.IsNullOrWhiteSpace(section) || string.IsNullOrWhiteSpace(department) || string.IsNullOrWhiteSpace(district) ||
                string.IsNullOrWhiteSpace(mandal) || string.IsNullOrWhiteSpace(village) || string.IsNullOrWhiteSpace(location))
            {
                ViewBag.AlertMessage = "Please select all required fields.";
                return await AllotWork();
            }

            // Validate AT Engineer exists
            var atEngineer = await _context.ATEngineer.FindAsync(atEngineerId);
            if (atEngineer == null)
            {
                ViewBag.AlertMessage = "AT Engineer not found.";
                return await AllotWork();
            }

            // Validate and process file upload if available
            byte[] fileData = null;
            if (postalCopy != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await postalCopy.CopyToAsync(memoryStream);
                    fileData = memoryStream.ToArray();  // Convert the file to binary
                }
            }
            else
            {
                ViewBag.AlertMessage = "Postal Copy file is required.";
                return await AllotWork();
            }

            // Create a new WorkEntry model instance
            var workEntry = new AttendencePageModel
            {
                postal_no = postalNo,
                postal_code = postalCode,
                postal_date = postalDate,
                postal_copy = fileData,
                EngineerId = atEngineerId,
                Section = section,
                Department = department,
                District = district,
                Mandal = mandal,
                Village = village,
                Name = atEngineer.Name, // Use the fetched AT Engineer's name
                Location = location
            };

            // Save the new entry in the database
            _context.AttendencePage.Add(workEntry);
            await _context.SaveChangesAsync();

            ViewBag.AlertMessage = "Work allotment successful!";
            return RedirectToAction("AllotWorkSuccess");
        }

        // GET: A simple success page after allotment
        public IActionResult AllotWorkSuccess()
        {
            return View("~/Views/Admin/AllotWorkSuccess.cshtml");
        }
    }
}
