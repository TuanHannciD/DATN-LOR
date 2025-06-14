#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class SanPham_ThanhToan
    {
        [Key]
        public Guid ID_Sp_ThanhToan { get; set; }

        [Required]
        public Guid ID_ThanhToan { get; set; }

        [Required]
        public Guid ID_Spct { get; set; }

        [Required]
        public float SoLuong { get; set; }

        [ForeignKey("ID_ThanhToan")]
        public virtual ThanhToan ThanhToan { get; set; }

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
    }
} 