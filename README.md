# 💻 Computer-Shop-Website

## 📌 Giới thiệu
Website thương mại điện tử bán PC, máy tính và linh kiện máy tính.  
Hệ thống cho phép người dùng duyệt sản phẩm, tìm kiếm, thêm vào giỏ hàng, đặt hàng và thanh toán trực tuyến.  
Admin có thể quản lý sản phẩm, danh mục, thương hiệu, người dùng, đơn hàng và thống kê.  

---

## 🚀 Tính năng chính
- Đăng ký, đăng nhập, phân quyền (Admin/Khách hàng)  
- Tìm kiếm & lọc sản phẩm theo **danh mục**, **thương hiệu**  
- Giỏ hàng, thanh toán  
- Quản lý người dùng, sản phẩm, danh mục, thương hiệu, đơn hàng  
- Thống kê và xuất dữ liệu (Excel, PDF, CSV)  
- Quản lý vai trò (Roles)  

---

## 🛠️ Công nghệ sử dụng
- **Front-end:** HTML, CSS, Bootstrap, JavaScript/jQuery  
- **Back-end:** ASP.NET MVC, ASP.NET Identity, Entity Framework (ORM)  
- **Database:** SQL Server  

---

## 📂 Cấu trúc thư mục (tham khảo)
Products-Ecommerce-Website/
├── Controllers/ # Xử lý logic
├── Models/ # Lớp dữ liệu (Entity Framework)
├── Views/ # Giao diện (Razor Views)
├── Scripts/ # JavaScript/jQuery
├── Content/ # CSS, Bootstrap
└── App_Data/ # SQL Database

yaml
Copy code

---

## ⚙️ Cài đặt & chạy dự án
1. Clone repo về máy:
   ```bash
   git clone https://github.com/danghongchi/Products-Ecommerce-Website.git
Mở project bằng Visual Studio

Cấu hình SQL Server trong Web.config (connection string)

Chạy lệnh Update-Database (nếu dùng Entity Framework Code First)

Chạy ứng dụng (IIS Express hoặc Kestrel)

📷 Hình ảnh giao diện

Trang chủ
<img width="979" height="521" alt="image" src="https://github.com/user-attachments/assets/bc999654-63f9-447c-80ad-ea07c1589d47" />
<img width="978" height="524" alt="image" src="https://github.com/user-attachments/assets/4d66f4e7-5204-47aa-b010-2f43398e350b" />

Chi tiết sản phẩm
<img width="980" height="521" alt="image" src="https://github.com/user-attachments/assets/e8e3699a-4036-4799-81ef-1a6a5e2c93f2" />
<img width="978" height="522" alt="image" src="https://github.com/user-attachments/assets/802b7615-5f9a-49ef-a6d7-19c72d259840" />

Giỏ hàng & Thanh toán
<img width="980" height="522" alt="image" src="https://github.com/user-attachments/assets/333917a6-e346-4186-ab2f-f8a516faa39c" />

Quản lý sản phẩm (Admin)
<img width="980" height="520" alt="image" src="https://github.com/user-attachments/assets/2018c6b1-3c21-4b85-99d7-4a3f96be4b30" />
<img width="980" height="520" alt="image" src="https://github.com/user-attachments/assets/9dec0342-9d4b-4e67-afa5-ae3c646bb599" />


📈 Hướng phát triển
Nâng cao UI/UX, thiết kế responsive

Tích hợp thanh toán trực tuyến (VNPay, Momo, Paypal)

Bổ sung chức năng quản lý khuyến mãi & mã giảm giá

Tăng cường tính năng bảo mật (2FA, reCAPTCHA)

Tối ưu hiệu suất & mở rộng hệ thống

👨‍💻 Sinh viên thực hiện: Trần Văn Chung, Đặng Hồng Chí
🎓 Trường: Đại học Nguyễn Tất Thành – Khoa CNTT
📅 Thời gian: 2024
