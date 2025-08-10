# TODO - DATN Lor System

## 🚀 Tính năng đã hoàn thành

### ✅ Quản lý sản phẩm
- [x] CRUD sản phẩm (Giay)
- [x] CRUD chi tiết sản phẩm (ChiTietGiay)
- [x] Quản lý chất liệu (ChatLieu)
- [x] Quản lý màu sắc (MauSac)
- [x] Quản lý kích thước (KichThuoc)
- [x] Quản lý thương hiệu (ThuongHieu)
- [x] Quản lý hình ảnh sản phẩm (AnhGiay)

### ✅ Quản lý người dùng
- [x] Đăng ký, đăng nhập
- [x] Phân quyền (Admin, Nhân viên, Khách hàng)
- [x] Quản lý thông tin tài khoản
- [x] Quản lý địa chỉ người dùng

### ✅ Hệ thống hóa đơn
- [x] Model HoaDon với enum TrangThaiHoaDon, PhuongThucThanhToan, PhuongThucVanChuyen
- [x] Hiển thị danh sách hóa đơn với tên tiếng Việt
- [x] Bán hàng tại quầy (BanHangTaiQuay)
- [x] Tìm kiếm và chọn khách hàng
- [x] Giảm giá theo sản phẩm (item-level discount)
- [x] Giảm giá theo hóa đơn (bill-level discount)
- [x] Validate client-side cho các modal giảm giá
- [x] Select2 dropdown với icon MDI cho phương thức thanh toán/vận chuyển

### ✅ Giao diện Admin
- [x] Layout Admin riêng biệt
- [x] Dashboard Admin
- [x] Navigation Admin
- [x] Quản lý User (UserManager)

---

## 🔄 Đang thực hiện

### 📋 Bán hàng tại quầy (BanHangTaiQuay)
- [ ] **Logic xóa giảm giá hóa đơn**: Nếu không nhập bất cứ dữ liệu gì vào modal giảm giá hóa đơn thì coi như xóa bỏ giảm giá
- [ ] **Phí vận chuyển**: Render phí vận chuyển khi KHÔNG tích vào ô "Đặt hàng" (giao hàng)
- [ ] **Phương thức thanh toán động**:
  - [ ] Nếu chọn "Tiền mặt" → render input nhập tiền khách trả → tính tiền trả lại
  - [ ] Nếu chọn "Chuyển khoản", "Thẻ tín dụng", "Ví điện tử" → giữ nguyên lựa chọn và lưu để xử lý sau

---

## 📝 Tính năng cần phát triển

### 🛒 Giỏ hàng & Đặt hàng
- [ ] Giỏ hàng cho khách hàng
- [ ] Thêm sản phẩm vào giỏ hàng
- [ ] Cập nhật số lượng trong giỏ hàng
- [ ] Xóa sản phẩm khỏi giỏ hàng
- [ ] Đặt hàng từ giỏ hàng

### 📊 Báo cáo & Thống kê
- [ ] Dashboard thống kê doanh thu
- [ ] Báo cáo sản phẩm bán chạy
- [ ] Báo cáo tồn kho
- [ ] Xuất báo cáo PDF/Excel
- [ ] Biểu đồ thống kê theo thời gian

### 🎨 Giao diện người dùng
- [ ] Trang chủ người dùng
- [ ] Danh sách sản phẩm
- [ ] Chi tiết sản phẩm
- [ ] Tìm kiếm và lọc sản phẩm
- [ ] Giỏ hàng người dùng
- [ ] Trang thanh toán
- [ ] Lịch sử đơn hàng

### 🔐 Bảo mật & Phân quyền
- [ ] Middleware xác thực
- [ ] Authorization theo vai trò
- [ ] JWT Token (nếu cần API)
- [ ] Logging và audit trail

### 📦 Quản lý kho
- [ ] Nhập kho
- [ ] Xuất kho
- [ ] Kiểm kê kho
- [ ] Cảnh báo tồn kho thấp
- [ ] Báo cáo tồn kho

### 🚚 Quản lý vận chuyển
- [ ] Cấu hình phí vận chuyển
- [ ] Theo dõi trạng thái giao hàng
- [ ] Tích hợp đơn vị vận chuyển
- [ ] Tính toán thời gian giao hàng

### 💰 Quản lý thanh toán
- [ ] Tích hợp cổng thanh toán
- [ ] Quản lý mã giảm giá
- [ ] Quản lý điểm tích lũy
- [ ] Hoàn tiền và đổi trả

### 📱 Responsive Design
- [ ] Tối ưu giao diện mobile
- [ ] Progressive Web App (PWA)
- [ ] Push notifications

### 🔧 Cấu hình hệ thống
- [ ] Cấu hình chung (appsettings)
- [ ] Quản lý cấu hình động
- [ ] Backup và restore database
- [ ] Logging và monitoring

---

## 🐛 Bug cần sửa

### 🔥 Ưu tiên cao
- [ ] Kiểm tra và sửa lỗi null reference trong LINQ queries
- [ ] Validate dữ liệu đầu vào tất cả các form
- [ ] Xử lý exception và error handling

### 🔧 Ưu tiên trung bình
- [ ] Tối ưu performance database queries
- [ ] Cải thiện UX/UI
- [ ] Unit testing

### 📝 Ưu tiên thấp
- [ ] Code documentation
- [ ] Refactoring code
- [ ] Performance optimization

---

## 📋 Công việc hiện tại

### 🎯 Focus chính
1. **Hoàn thiện Bán hàng tại quầy**:
   - Logic xóa giảm giá hóa đơn
   - Phí vận chuyển động
   - UI thanh toán theo phương thức

2. **Phát triển giỏ hàng người dùng**:
   - Model và controller
   - Giao diện giỏ hàng
   - Logic thêm/xóa/cập nhật

3. **Báo cáo và thống kê**:
   - Dashboard admin
   - Biểu đồ doanh thu
   - Báo cáo sản phẩm

---

## 📅 Timeline dự kiến

### Tuần 1-2: Hoàn thiện Bán hàng tại quầy
- [ ] Logic xóa giảm giá hóa đơn
- [ ] Phí vận chuyển động
- [ ] UI thanh toán theo phương thức

### Tuần 3-4: Giỏ hàng người dùng
- [ ] Model và controller
- [ ] Giao diện giỏ hàng
- [ ] Logic thêm/xóa/cập nhật

### Tuần 5-6: Báo cáo và thống kê
- [ ] Dashboard admin
- [ ] Biểu đồ doanh thu
- [ ] Báo cáo sản phẩm

### Tuần 7-8: Testing và tối ưu
- [ ] Unit testing
- [ ] Performance optimization
- [ ] Bug fixes

---

## 📝 Ghi chú

- **Ưu tiên**: Hoàn thiện tính năng bán hàng tại quầy trước
- **Testing**: Viết test case cho các tính năng quan trọng
- **Documentation**: Cập nhật README và API documentation
- **Security**: Kiểm tra và cải thiện bảo mật

---

*Cập nhật lần cuối: [Ngày hiện tại]* 