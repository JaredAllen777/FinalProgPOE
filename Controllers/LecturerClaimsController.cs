using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractPoe.Models;

namespace ContractPoe.Controllers
{
    public class LecturerClaimsController : Controller
    {
        private readonly AppDbContext _context;

        public LecturerClaimsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LecturerClaims
        public async Task<IActionResult> Index()
        {
            var claims = await _context.LecturerClaims.ToListAsync();
            return View(claims);
        }

        // GET: LecturerClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerClaim = await _context.LecturerClaims
                .FirstOrDefaultAsync(m => m.ClaimId == id);
            if (lecturerClaim == null)
            {
                return NotFound();
            }

            return View(lecturerClaim);
        }

        // GET: LecturerClaims/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimId,HoursWorked,HourlyRate,AdditionalNotes")] LecturerClaim lecturerClaim, IFormFile? DocumentPath)
        {
            if (DocumentPath != null)
            {
                // Define the path where the file will be saved
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Ensure the upload directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(DocumentPath.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await DocumentPath.CopyToAsync(stream);
                }

                // Set the document path in the model
                lecturerClaim.DocumentPath = "/uploads/" + fileName;
            }

            // Validate and save the claim data
            if (ModelState.IsValid)
            {
                _context.Add(lecturerClaim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(lecturerClaim);
        }


        // GET: LecturerClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerClaim = await _context.LecturerClaims.FindAsync(id);
            if (lecturerClaim == null)
            {
                return NotFound();
            }
            return View(lecturerClaim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimId,HoursWorked,HourlyRate,AdditionalNotes,DocumentPath")] LecturerClaim lecturerClaim)
        {
            if (id != lecturerClaim.ClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lecturerClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LecturerClaimExists(lecturerClaim.ClaimId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lecturerClaim);
        }

        // GET: LecturerClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lecturerClaim = await _context.LecturerClaims
                .FirstOrDefaultAsync(m => m.ClaimId == id);
            if (lecturerClaim == null)
            {
                return NotFound();
            }

            return View(lecturerClaim);
        }

        // POST: LecturerClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturerClaim = await _context.LecturerClaims.FindAsync(id);
            if (lecturerClaim != null)
            {
                _context.LecturerClaims.Remove(lecturerClaim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LecturerClaimExists(int id)
        {
            return _context.LecturerClaims.Any(e => e.ClaimId == id);
        }
    }
}
