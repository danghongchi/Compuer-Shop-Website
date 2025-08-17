# 🛒 Products E-commerce Website

Website thương mại điện tử bán sản phẩm công nghệ (PC, laptop, linh kiện) được xây dựng bằng **ReactJS (Frontend)**, **Node.js + Express (Backend)** và **MySQL (Database)**.  

## 📌 Giới thiệu
Người dùng có thể đăng ký/đăng nhập tài khoản, duyệt và tìm kiếm sản phẩm theo danh mục hoặc hãng, xem chi tiết sản phẩm, thêm vào giỏ hàng và đặt hàng.  
Admin có quyền quản lý người dùng (bao gồm xóa tài khoản).  

## 🚀 Tính năng chính
- Đăng ký, đăng nhập, đăng xuất  
- Tìm kiếm & lọc sản phẩm theo **danh mục** hoặc **hãng**  
- Xem chi tiết sản phẩm  
- Quản lý giỏ hàng: thêm, xóa, đặt hàng  
- Xem danh sách đơn hàng đã mua  
- Đổi mật khẩu, xóa tài khoản (người dùng)  
- Quản lý tài khoản người dùng (Admin)  

## 🛠️ Công nghệ sử dụng
- **Frontend:** ReactJS, HTML, CSS, JavaScript, Bootstrap  
- **Backend:** Node.js, ExpressJS (REST API)  
- **Database:** MySQL (XAMPP, user `root`, không mật khẩu)  

## 📂 Cấu trúc thư mục
products-ecommerce-website/
├── client/ # ReactJS frontend
│ ├── public/
│ └── src/
│ ├── components/
│ ├── pages/
│ ├── assets/
│ └── App.jsx
├── server/ # Node.js backend (Express)
│ ├── routes/
│ ├── models/
│ ├── controllers/
│ ├── config/
│ └── server.js
└── docs/ # Tài liệu, hình ảnh minh họa
└── images/

bash
Copy code

## ⚙️ Hướng dẫn cài đặt
1. **Clone project**
   ```bash
   git clone https://github.com/danghongchi/Products-Ecommerce-Website.git
Backend (server)

bash
Copy code
cd server
npm install
npm run dev
API chạy tại: http://localhost:5000

Frontend (client)

bash
Copy code
cd client
npm install
npm run dev
Web chạy tại: http://localhost:5173 (hoặc port hiển thị).

📷 Hình ảnh giao diện

Trang chủ
<img width="950" height="535" alt="image" src="https://github.com/user-attachments/assets/81072d8f-ee9e-431b-9c56-139a175b7c32" />

Trang chi tiết sản phẩm
<img width="950" height="534" alt="image" src="https://github.com/user-attachments/assets/6bb46b40-3d18-4272-9933-04da96d67878" />

Trang giỏ hàng
<img width="950" height="535" alt="image" src="https://github.com/user-attachments/assets/393b2452-dd66-4dad-aa2b-f2fa9fbcf498" />

Trang quản trị người dùng
<img width="950" height="535" alt="image" src="https://github.com/user-attachments/assets/2b79dc47-c676-4ce2-bdc2-8f82f598d502" />

📈 Hướng phát triển
Tích hợp cổng thanh toán trực tuyến (VNPay, Momo, Paypal)

Thêm chức năng quản lý sản phẩm & đơn hàng cho admin

Cải thiện UI/UX và hiệu năng
