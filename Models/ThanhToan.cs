#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class ThanhToan
    {
        [Key]
        public Guid ID_ThanhToan { get; set; }

        [Required]
        public Guid ID_HoaDon { get; set; }

        [Required]
        [StringLength(50)]
        public required string PhuongThucThanhToan { get; set; }

        [Required]
        [StringLength(30)]
        public required string Status { get; set; }

        [Required]
        public float SoTienThanhToan { get; set; }

        [StringLength(30)]
        public string? DiaChi { get; set; }

        [StringLength(15)]
        public string? SDT { get; set; }

        [StringLength(20)]
        public string? HoTen { get; set; }

        [StringLength(255)]
        public string? GhiChu { get; set; }

        [ForeignKey("ID_HoaDon")]
        public virtual HoaDon HoaDon { get; set; }
    }
} 