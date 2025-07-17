namespace AuthDemo.Models.ViewModels
{
    public class BanHangTaiQuayVM
    {
        public Guid ShoeDetailID { get; set; }
        public string? TenSp { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public string? MauSac { get; set; }
        public string? KichThuoc { get; set; }
        public string? ChatLieu { get; set; }
        public string? ThuongHieu { get; set; }
        public string? DanhMuc { get; set; }
        public string? Giay { get; set; }
    }

    public class KhachHangDropdownVM
    {
        public Guid UserID { get; set; }
        public string TenDangNhap { get; set; } // Số điện thoại hoặc username
        public string? HoTen { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string TenVaiTro { get; set; } // Để xác định là "user"
    }
} 