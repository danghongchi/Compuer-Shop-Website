using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ComputerShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _dataContext = context;
        }

        public IActionResult Index()
        {
            var products = _dataContext.Products.Include("Category").Include("Brand").ToList();
            var sliders = _dataContext.Sliders.Where(s => s.Status==1).ToList(); // Lọc slider có status là true
            ViewBag.Sliders = sliders; // Đưa slider vào ViewBag
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            var contact = await _dataContext.Contacts.FirstOrDefaultAsync();
            if (contact == null)
            {
                // Xử lý khi không có dữ liệu, ví dụ hiển thị thông báo
                TempData["ErrorMessage"] = "No contact information available.";
                return View(); // Có thể trả về một view khác hoặc view trống
            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404) 
            {
                return View("NotFoud");
            }
            else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
           
        }
    }
}
