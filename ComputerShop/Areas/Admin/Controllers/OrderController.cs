using ComputerShop.Models;
using ComputerShop.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Publish")]
    [Route("Admin/Order")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;

        public OrderController(DataContext context)
        {
            _dataContext = context;
        }

        // Action để hiển thị danh sách đơn hàng
        [HttpGet("Index")]
        public async Task<IActionResult> Index(int pg = 1)
        {
            List<OrderModel> order = await _dataContext.Orders.ToListAsync(); // Đảm bảo async
            const int pageSize = 10;

            if (pg < 1)
            {
                pg = 1;
            }

            int recsCount = order.Count();
            var pager = new Paginate(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = order.Skip(recSkip).Take(pager.PageSize).ToList();
            ViewBag.Pager = pager;

            return View(data);
        }

        // Action để xem chi tiết đơn hàng
        [HttpGet("ViewOrder/{ordercode}")]
        public async Task<IActionResult> ViewOrder(string ordercode)
        {
            var detailsOrder = await _dataContext.OrderDetails
                                .Include(od => od.Product)
                                .Where(od => od.OrderCode == ordercode)
                                .ToListAsync();

            return View(detailsOrder); // Trả về danh sách chi tiết đơn hàng
        }

        // Action để cập nhật trạng thái đơn hàng
        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(string orderCode, int status)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);
            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }

            order.Status = status;
            await _dataContext.SaveChangesAsync();

            return Json(new { success = true });
        }

        // Thêm Action Delete để xóa đơn hàng
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(string orderCode)
        {
            // Tìm đơn hàng bằng OrderCode
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderCode == orderCode);

            if (order == null)
            {
                return Json(new { success = false, message = "Order not found" });
            }

            // Xóa chi tiết đơn hàng trước khi xóa đơn hàng
            var orderDetails = await _dataContext.OrderDetails.Where(od => od.OrderCode == orderCode).ToListAsync();
            _dataContext.OrderDetails.RemoveRange(orderDetails);

            // Xóa đơn hàng
            _dataContext.Orders.Remove(order);

            await _dataContext.SaveChangesAsync();

            return Json(new { success = true, message = "Order deleted successfully" });
        }
    }
}
