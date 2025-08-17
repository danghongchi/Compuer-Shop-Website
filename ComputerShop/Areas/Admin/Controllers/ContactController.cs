using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Publish")]
    [Route("Admin/Contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContactController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var contact = _dataContext.Contacts.ToList();
            return View(contact);
        }

        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ContactModel contact = await _dataContext.Contacts.FirstOrDefaultAsync();
            return View(contact);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactModel contact)
        {
            var existed_contact = await _dataContext.Contacts.FirstOrDefaultAsync();
            if (existed_contact == null)
            {
                TempData["error"] = "Liên hệ không tồn tại.";
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // Kiểm tra và xử lý upload logo mới
                if (contact.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/logos");
                    string imageName = Guid.NewGuid().ToString() + "_" + contact.ImageUpload.FileName;
                    string filePath = Path.Combine(uploadsDir, imageName);

                    // Xóa logo cũ nếu tồn tại
                    if (!string.IsNullOrEmpty(existed_contact.LogoImg))
                    {
                        string oldfileImage = Path.Combine(uploadsDir, existed_contact.LogoImg);
                        if (System.IO.File.Exists(oldfileImage))
                            System.IO.File.Delete(oldfileImage);
                    }

                    // Lưu logo mới
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        await contact.ImageUpload.CopyToAsync(fs);
                    }

                    existed_contact.LogoImg = imageName;
                }

                // Cập nhật các thông tin khác
                existed_contact.Name = contact.Name;
                existed_contact.Email = contact.Email;
                existed_contact.Description = contact.Description;
                existed_contact.Phone = contact.Phone;
                existed_contact.Map = contact.Map;

                _dataContext.Update(existed_contact);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Cập nhật liên hệ thành công.";
                return RedirectToAction("Index");
            }

            // Xử lý lỗi và thông báo chi tiết
            TempData["error"] = "Có lỗi xảy ra: ";
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    TempData["error"] += error.ErrorMessage + " ";
                }
            }

            return View(contact);
        }
    }
}
