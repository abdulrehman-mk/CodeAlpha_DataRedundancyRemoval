using System.Diagnostics;
using CodeAlpha_DataRedundancyRemoval.Data;
using CodeAlpha_DataRedundancyRemoval.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeAlpha_DataRedundancyRemoval.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CategoryCount = await _context.Categories.CountAsync();
            ViewBag.ProductCount = await _context.Products.CountAsync();
            ViewBag.CustomerCount = await _context.Customers.CountAsync();
            ViewBag.OrderCount = await _context.Orders.CountAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
