#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class KhuyenMai
    {
        [Key]
        public Guid Ma_Km { get; set; }

        [Required]
        [StringLength(100)]
        public required string TenKm { get; set; }

        [StringLength(300)]
        public string? MoTa { get; set; }

        [Required]
        [StringLength(50)]
        public required string LoaiKm { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal GiaTriKm { get; set; }

        [Required]
        public DateTime NgayBd { get; set; }

        [Required]
        public DateTime NgayKt { get; set; }

        public int? SoLuong { get; set; }

        public int? SoLuong1Ng { get; set; }

        [Required]
        [StringLength(30)]
        public required string HoTenAdmin { get; set; }

        [ForeignKey("HoTenAdmin")]
        public virtual Admin Admin { get; set; }
    }
} 