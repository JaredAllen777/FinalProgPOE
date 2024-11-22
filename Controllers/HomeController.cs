using ContractPoe.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContractPoe.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View(); // This will render the "Welcome" page (Home/Index.cshtml)
        }
    }
}


// public IActionResult Privacy()
//{
//    return View();
// }

//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   //     public IActionResult Error()
      //  {
     //       return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      //  }
  //  }
//}
