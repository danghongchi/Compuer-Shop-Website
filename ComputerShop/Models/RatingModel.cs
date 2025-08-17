using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
	public class RatingModel
	{
		[Key]
		public int Id { get; set; }
	
		public int ProductId { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập ")]
		public string Comment { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập ")]
		public string Name { get; set; }
		
		[Required(ErrorMessage = "Vui lòng nhập ")]
		public string Email { get; set; }
		public string Star { get; set; }
		[ForeignKey("ProductId")]

		public ProductModel Product { get; set; }

	}
}
