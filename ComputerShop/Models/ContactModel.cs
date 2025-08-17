using ComputerShop.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class ContactModel
    {
        [Key]
        [Required(ErrorMessage = "Yêu cầu nhập tiêu đề.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập bản đồ.")]
        public string Map { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập thông tin liên hệ.")]
        public string Description { get; set; }

        public string? LogoImg { get; set; }

        [NotMapped]
        [FileExtension(ErrorMessage = "Chỉ chấp nhận các tệp có định dạng hình ảnh.")]
        public IFormFile? ImageUpload { get; set; }
    }
}
