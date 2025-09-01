using System.Text.Json.Serialization;
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
        public bool DaThanhToan { get; set; } // map với cột bit trong DB
        public DateTime? NgayCapNhat { get; set; }
        public string? TenKhachHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public PhuongThucThanhToan HinhThucThanhToan { get; set; }
        public PhuongThucVanChuyen HinhThucVanChuyen { get; set; }
        public TrangThaiHoaDon TrangThaiHoaDon { get; set; }
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
        public DateTime? NgayTao { get; set; }
        public string PhuongThucThanhToan { get; set; }
        public string PhuongThucVanChuyen { get; set; }
    }

    public class CreateHoaDonVM
    {

        [JsonPropertyName("userID")]
        public Guid? UserID { get; set; }
        [JsonPropertyName("hinhThucThanhToan")]
        public string HinhThucThanhToan { get; set; }
        [JsonPropertyName("hinhThucVanChuyen")]
        public string HinhThucVanChuyen { get; set; }
        [JsonPropertyName("giamGiaPhanTram")]
        public decimal? GiamGiaPhanTram { get; set; }
        [JsonPropertyName("giamGiaTienMat")]
        public decimal? GiamGiaTienMat { get; set; }
        [JsonPropertyName("lyDo")]

        public string? LyDo { get; set; }
    }

    public class HoaDonTongTienVM
    {
        public decimal TongTienHang { get; set; }
        public decimal GiamGiaHoaDon { get; set; }
        public decimal TongThanhToan { get; set; }

    }
    public class IDHoaDon
    {
        public Guid ID { get; set; }
    }
}
