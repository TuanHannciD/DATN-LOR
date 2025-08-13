using AuthDemo.Models.Enums;

namespace AuthDemo.Models.ViewModels
{
    public class GetAllHoaDonVM
    {
        public Guid HoaDonID { get; set; }
        public Guid UserID { get; set; }
        public decimal TongTien { get; set; }
        public string? NguoiTao { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? NguoiCapNhat { get; set; }
        public DateTime? NgayCapNhat { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public PhuongThucThanhToan HinhThucThanhToan { get; set; }
        public PhuongThucVanChuyen HinhThucVanChuyen { get; set; }
        // Thêm các trường display
        public string TrangThaiDisplay { get; set; }
        public string HinhThucThanhToanDisplay { get; set; }
        public string HinhThucVanChuyenDisplay { get; set; }
    }
    public class HoaDonDTO
    {
        public Guid BillID { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public decimal TongTien { get; set; }
        public DateTime ?NgayTao { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string PhuongThucVanChuyen { get; set; }
    }
    
    public class CreateHoaDonVM
    {

        public Guid UserID { get; set; }

        public string HinhThucThanhToan { get; set; }
        public string HinhThucVanChuyen { get; set; }
        public decimal? GiamGiaPhanTram { get; set; }
        public decimal? GiamGiaTienMat { get; set; }
        public string? LyDo { get; set; }
    }
    
    public class HoaDonTongTienVM
    {
        public decimal TongTienHang { get; set; }
        public decimal GiamGiaHoaDon { get; set; }
        public decimal TongThanhToan { get; set; }

    }
}