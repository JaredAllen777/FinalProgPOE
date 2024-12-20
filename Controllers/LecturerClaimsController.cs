﻿using System;
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
            _context = context;  // Store the DbContext instance
        }

        public async Task<IActionResult> TrackClaim()
        {
            var allClaims = await _context.LecturerClaims
                                          .Include(c => c.Lecturer)  // This line loads the Lecturer data as well
                                          .OrderByDescending(c => c.ClaimId)
                                          .ToListAsync();

            return View(allClaims);
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
            // Custom Validation Logic
            if (lecturerClaim.HoursWorked > 40)
            {
                ModelState.AddModelError("HoursWorked", "Hours worked cannot exceed 40 per week.");
            }

            if (lecturerClaim.HourlyRate < 15 || lecturerClaim.HourlyRate > 50)
            {
                ModelState.AddModelError("HourlyRate", "Hourly rate must be between $15 and $50.");
            }

            if (string.IsNullOrWhiteSpace(lecturerClaim.AdditionalNotes) || lecturerClaim.AdditionalNotes.Length > 500)
            {
                ModelState.AddModelError("AdditionalNotes", "Notes cannot exceed 500 characters.");
            }

            if (DocumentPath == null)
            {
                ModelState.AddModelError("DocumentPath", "You must upload a supporting document.");
            }

            if (ModelState.IsValid)
            {
                // Save file to the server
                if (DocumentPath != null)
                {
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    string fileName = Guid.NewGuid() + Path.GetExtension(DocumentPath.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await DocumentPath.CopyToAsync(stream);
                    }
                    lecturerClaim.DocumentPath = "/uploads/" + fileName;
                }

                // Add the claim to the context
                _context.Add(lecturerClaim);
                await _context.SaveChangesAsync();

                // Debugging check: Verify if the claim was saved
                var savedClaims = await _context.LecturerClaims.ToListAsync();
                System.Diagnostics.Debug.WriteLine($"Total Claims Saved: {savedClaims.Count}"); // This will show in the Output window in Visual Studio

                // Success message and redirect
                TempData["SuccessMessage"] = "Claim submitted successfully!";
                return RedirectToAction(nameof(Index));
            }

            // If model state is invalid, return the view with validation errors
            return View(lecturerClaim);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var claim = await _context.LecturerClaims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            claim.IsApproved = true;
            _context.Update(claim);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Claim approved successfully!";
            return RedirectToAction(nameof(Index));
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
