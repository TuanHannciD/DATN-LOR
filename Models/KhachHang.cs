#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class KhachHang
    {
        [Required]
        [StringLength(30)]
        public required string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public required string Sdt { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [StringLength(255)]
        public string? DiaChi { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        [Required]
        [StringLength(10)]
        public required string TrangThai { get; set; }

        public DateTime NgayDangKy { get; set; }

        public int DiemTichLuy { get; set; }

        [Required]
        [StringLength(20)]
        public required string LoaiKhachHang { get; set; }

        [StringLength(30)]
        public string? HoTenAdmin { get; set; }

        [Required]
        [StringLength(30)]
        public required string UserName { get; set; }

        [ForeignKey("HoTenAdmin")]
        public virtual Admin? Admin { get; set; }

        [ForeignKey("UserName")]
        public virtual User User { get; set; }
    }
} 