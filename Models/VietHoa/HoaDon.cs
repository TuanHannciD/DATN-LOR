using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class HoaDon : ThongTinChung
    {
        [Key]
        public Guid BillID { get; set; } // giữ nguyên
        [Required]
        public Guid UserID { get; set; } // giữ nguyên
        [StringLength(100)]
        public string HoTen { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string SoDienThoai { get; set; }
        [StringLength(255)]
        public string DiaChi { get; set; }
        public decimal TongTien { get; set; }
        [StringLength(50)]
        public string TrangThai { get; set; }
        public bool DaThanhToan { get; set; }
        [StringLength(50)]
        public string PhuongThucThanhToan { get; set; }
        public bool DaHuy { get; set; }
        [StringLength(255)]
        public string GhiChu { get; set; }
        public DateTime? NgayGiaoHang { get; set; }
        [StringLength(255)]
        public string LyDo { get; set; }
    }
} 