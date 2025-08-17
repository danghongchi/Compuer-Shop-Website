using System.ComponentModel.DataAnnotations.Schema;

namespace ComputerShop.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderCode { get; set; }
        public int ProductId { get; set; }  // Đổi từ long thành int
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }



}

