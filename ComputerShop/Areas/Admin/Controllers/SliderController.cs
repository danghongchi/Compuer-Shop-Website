using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ComputerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Slider")]
    [Authorize(Roles = "Admin,Publish")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Sliders.OrderByDescending(p => p.Id).ToListAsync());
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
        public async Task<IActionResult> Create(SliderModel slider)
        {
            if (ModelState.IsValid)
            {
                if (slider.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/sliders");
                    string imageName = Guid.NewGuid() + "_" + slider.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await slider.ImageUpload.CopyToAsync(fs);
                    }

                    slider.Image = imageName;
                }

                _dataContext.Add(slider);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Thêm slider thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang bị lỗi";
                return View(slider);
            }
        }

        [HttpGet]
        [Route("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _dataContext.Sliders.FindAsync(id);
            if (slider == null)
            {
                TempData["error"] = "Slider không tồn tại";
                return NotFound();
            }
            return View(slider);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderModel slider)
        {
            var slider_existed = await _dataContext.Sliders.FindAsync(slider.Id);
            if (slider_existed == null)
            {
                TempData["error"] = "Slider không tồn tại";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (slider.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/sliders");
                    string imageName = Guid.NewGuid() + "_" + slider.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await slider.ImageUpload.CopyToAsync(fs);
                    }

                    slider_existed.Image = imageName;
                }

                slider_existed.Name = slider.Name;
                slider_existed.Description = slider.Description;
                slider_existed.Status = slider.Status;

                _dataContext.Update(slider_existed);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật slider thành công";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang bị lỗi";
                return View(slider);
            }
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var slider = await _dataContext.Sliders.FindAsync(id);
            if (slider == null)
            {
                TempData["error"] = "Slider không tồn tại";
                return NotFound();
            }

            if (!string.IsNullOrEmpty(slider.Image))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/sliders");
                string oldfileImage = Path.Combine(uploadsDir, slider.Image);
                if (System.IO.File.Exists(oldfileImage))
                {
                    System.IO.File.Delete(oldfileImage);
                }
            }

            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();
            TempData["success"] = "Slider đã xóa";
            return RedirectToAction("Index");
        }
    }
}
