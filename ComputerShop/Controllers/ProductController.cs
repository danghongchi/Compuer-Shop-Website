using ComputerShop.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Controllers
{
    public class ProductController: Controller
    {
		private readonly DataContext _dataContext;
		public ProductController(DataContext context)
		{
			_dataContext = context;
		}
		public IActionResult Index()
        {
            return View();
        }
		public async Task<IActionResult> Details(int Id)
		{
			if (Id <= 0) return RedirectToAction("Index");

			// Lấy sản phẩm theo ID
			var productsById = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == Id);

			// Nếu sản phẩm không tồn tại, chuyển hướng về trang Index
			if (productsById == null)
			{
				return RedirectToAction("Index");
			}

			// Lấy sản phẩm liên quan
			var relatedProducts = await _dataContext.Products
				.Where(p => p.CategoryId == productsById.CategoryId && p.Id != productsById.Id)
				.Take(4)
				.ToListAsync();

			ViewBag.RelatedProducts = relatedProducts;

			return View(productsById);
		}


		public async Task<IActionResult> Search(string searchTerm)
		{
			var products = await _dataContext.Products
			.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
			.ToListAsync();
			ViewBag.Keyword = searchTerm;
			return View(products);
		}
	}
}
