#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class NhanVien
    {
        [Key]
        public Guid MaNV { get; set; }

        [Required]
        [StringLength(30)]
        public required string HoTenNV { get; set; }

        [Required]
        [StringLength(30)]
        public required string HoTenAdmin { get; set; }

        [Required]
        [StringLength(20)]
        public required string Sdt { get; set; }

        [StringLength(30)]
        public string? Email { get; set; }

        [StringLength(30)]
        public string? DiaChi { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        public string? GioiTinh { get; set; }

        [Required]
        [StringLength(30)]
        public required string TrangThai { get; set; }

        [Required]
        public DateTime NgayVaoLam { get; set; }

        [Required]
        [StringLength(50)]
        public required string ChucVu { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LuongCoBan { get; set; }

        [Required]
        public int SoGioLamViec { get; set; }

        [Required]
        [StringLength(50)]
        public required string UserName { get; set; }

        [ForeignKey("HoTenAdmin")]
        public virtual Admin Admin { get; set; }

        [ForeignKey("UserName")]
        public virtual User User { get; set; }
    }
} 