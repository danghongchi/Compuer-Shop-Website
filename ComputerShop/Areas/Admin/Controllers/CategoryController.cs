using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace ComputerShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,Publish")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        public CategoryController(DataContext context)
        {
            _dataContext = context;

        }
        [Route("Index")]
        public async Task<IActionResult> Index(int pg=1)
        {
            List<CategoryModel> category = _dataContext.Categories.ToList();
            const int pageSize = 10;


            if(pg < 1)
            {
                pg = 1;
            }
            int recsCount = category.Count();
            var pager = new Paginate(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = category.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;
            return View(data);
        }
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {

            if (ModelState.IsValid)
            {
                //code them du lieu
                category.Slug = category.Name.Replace(" ", "-");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Danh mục  đã có trong database");
                    return View(category);
                }


                _dataContext.Add(category);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm danh muc thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(category);

        }
       
        [HttpPost]

        [Route("Delete/{Id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);
            if (category == null)
            {
                return NotFound();
            }

            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Đã xóa danh mục";
            return RedirectToAction("Index");
        }
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);
            return View(category);
        }
        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = await _dataContext.Categories.FindAsync(category.Id);
                if (existingCategory == null)
                {
                    return NotFound();
                }

                // Kiểm tra slug mới có trùng với danh mục khác không
                var slugExists = await _dataContext.Categories
                                  .AnyAsync(p => p.Slug == category.Slug && p.Id != category.Id);
                if (slugExists)
                {
                    ModelState.AddModelError("", "Slug đã có trong database");
                    return View(category);
                }

                // Cập nhật các thuộc tính của danh mục
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;
                existingCategory.Status = category.Status;
                existingCategory.Slug = category.Name.Replace(" ", "-");

                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật danh mục thành công";
                return RedirectToAction("Index");
            }

            TempData["error"] = "Model có một vài thứ đang bị lỗi";
            return View(category);
        }

    }
}

