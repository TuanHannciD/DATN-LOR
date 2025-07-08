using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class NguoiDung : ThongTinChung
    {
        [Key]
        public Guid UserID { get; set; } // giữ nguyên
        [Required]
        [StringLength(50)]
        public string TenDangNhap { get; set; } // UserName
        [Required]
        [StringLength(100)]
        public string MatKhau { get; set; } // Password
        [StringLength(100)]
        public string HoTen { get; set; } // FullName
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string AnhDaiDien { get; set; } // ImgName
        [StringLength(20)]
        public string SoDienThoai { get; set; }
        [StringLength(100)]
        public string Token { get; set; }
        public DateTime? NgayHetHanToken { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<DiaChi> DiaChis { get; set; }
        public ICollection<GioHang> GioHangs { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
        public ICollection<LichSuHoaDon> LichSuHoaDons { get; set; }
        public ICollection<VaiTroNguoiDung> VaiTroNguoiDungs { get; set; }
    }
} 