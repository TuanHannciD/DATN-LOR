using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class ChiTietHoaDon : ThongTinChung
    {
        [Key]
        public Guid BillDetailID { get; set; } // giữ nguyên
        [Required]
        public Guid BillID { get; set; } // giữ nguyên
        [Required]
        public Guid ShoeDetailID { get; set; } // giữ nguyên
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
    }
} 