using Microsoft.AspNetCore.Mvc;
using ContractPoe.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ContractPoe.Controllers
{
    public class HRController : Controller
    {
        private readonly AppDbContext _context;

        public HRController(AppDbContext context)
        {
            _context = context;
        }

        // View Lecturer Claims and Data
        public async Task<IActionResult> Index()
        {
            var claims = await _context.LecturerClaims.Include(c => c.Lecturer).ToListAsync();
            return View(claims);
        }


        // Approve or Reject a Claim
        [HttpPost]
        public async Task<IActionResult> ProcessClaim(int claimId, bool approve)
        {
            var claim = await _context.LecturerClaims.FindAsync(claimId);
            if (claim != null)
            {
                claim.IsApproved = approve;
                _context.Update(claim);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Manage Lecturer Data (view/edit/delete)
        public async Task<IActionResult> ManageLecturerData()
        {
            var lecturers = await _context.LecturerClaims.ToListAsync();
            return View(lecturers);
        }

        // Add other HR-specific actions as needed
    }
}
