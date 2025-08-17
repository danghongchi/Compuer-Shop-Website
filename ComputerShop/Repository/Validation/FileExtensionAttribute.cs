using System.ComponentModel.DataAnnotations;
using System.IO; // Thêm thư viện này nếu thiếu
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ComputerShop.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string[] extensions = { "jpg", "jpeg", "png", "gif" };

                // Kiểm tra nếu không phải là định dạng hợp lệ
                if (!extensions.Contains(extension.TrimStart('.')))
                {
                    return new ValidationResult("Chỉ chấp nhận các định dạng ảnh (jpg, jpeg, png, gif)");
                }
            }
            return ValidationResult.Success;
        }

    }
}
