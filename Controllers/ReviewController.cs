using ContractPoe.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractPoe.Controllers
{
    public class ReviewController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var claims = _context.LecturerClaims.ToList();
            return View(claims);
        }

        public IActionResult Approve(int id)
        {
            var claim = _context.LecturerClaims.Find(id);
            if (claim != null)
            {
                // Logic for approval
                TempData["SuccessMessage"] = "Claim approved successfully!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}