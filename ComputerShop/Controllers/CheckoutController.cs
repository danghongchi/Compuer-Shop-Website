using ComputerShop.Areas.Admin.Repository; // Đảm bảo đúng namespace
using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ComputerShop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IAppEmailSender _emailSender; // Sử dụng interface mới

        public CheckoutController(IAppEmailSender emailSender, DataContext context)
        {
            _emailSender = emailSender;
            _dataContext = context;
        }

        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var ordercode = Guid.NewGuid().ToString();
                var orderItem = new OrderModel
                {
                    OrderCode = ordercode,
                    UserName = userEmail,
                    Status = 1,
                    CreateDate = DateTime.Now
                };
                _dataContext.Orders.Add(orderItem);
                await _dataContext.SaveChangesAsync();

                List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();

                foreach (var cart in cartItems)
                {
                    var orderdetails = new OrderDetails
                    {
                        UserName = userEmail,
                        OrderCode = ordercode,
                        ProductId = cart.ProductId,
                        Price = cart.Price,
                        Quantity = cart.Quantity
                    };
                    _dataContext.OrderDetails.Add(orderdetails);
                }

                await _dataContext.SaveChangesAsync();

                HttpContext.Session.Remove("Cart");

                // Gửi email
                var receiver = userEmail; // Gửi tới email của người dùng
                var subject = "Đặt hàng thành công";
                var message = "Cảm ơn bạn đã đặt hàng! Mã đơn hàng của bạn là: " + ordercode;
                await _emailSender.SendEmailAsync(receiver, subject, message);

                TempData["success"] = "Checkout thành công, vui lòng chờ duyệt đơn hàng!";
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
