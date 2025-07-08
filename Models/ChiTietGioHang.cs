using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class ChiTietGioHang : ThongTinChung
    {
        [Key]
        public Guid CartDetailID { get; set; } // giữ nguyên
        [Required]
        public Guid CartID { get; set; } // giữ nguyên
        
        [Required]
        public Guid ShoeDetailID { get; set; } // giữ nguyên
        public int SoLuong { get; set; }
        [StringLength(50)]
        public string KichThuoc { get; set; }
        public GioHang GioHang { get; set; }
        public ChiTietGiay ChiTietGiay { get; set; }
    }
} 