#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class SanPhamYeuThichChiTiet
    {
        [Key]
        public Guid ID_Spyt_Chi_Tiet { get; set; }

        [Required]
        public Guid ID_Spyt { get; set; }

        [Required]
        public Guid ID_Spct { get; set; }

        [Required]
        public float Gia { get; set; }

        [ForeignKey("ID_Spyt")]
        public virtual SanPhamYeuThich SanPhamYeuThich { get; set; }

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
    }
} 