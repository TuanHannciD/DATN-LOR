#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class SanPham_Mua
    {
        [Key]
        public Guid ID_SP_Mua { get; set; }

        [Required]
        public Guid ID_Spct { get; set; }

        [Required]
        [StringLength(30)]
        public required string UserName { get; set; }

        [Required]
        public float SoLuong { get; set; }

        [Required]
        public float Gia { get; set; }

        [Required]
        public Guid Ma_Km { get; set; }

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }

        [ForeignKey("Ma_Km")]
        public virtual KhuyenMai KhuyenMai { get; set; }

        [ForeignKey("UserName")]
        public virtual User User { get; set; }
    }
} 