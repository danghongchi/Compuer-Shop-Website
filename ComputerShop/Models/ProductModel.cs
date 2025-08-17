using ComputerShop.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }  // Đổi từ int thành long

        [Required(ErrorMessage = "Yêu cầu nhập tên Sản phẩm")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mô tả sản phẩm")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá sản phẩm phải lớn hơn 0")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "Chọn một thương hiệu")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn một thương hiệu hợp lệ")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Chọn một danh mục")]
        [Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục hợp lệ")]
        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }
        public BrandModel Brand { get; set; }

        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile? ImageUpload { get; set; }
    }


}
