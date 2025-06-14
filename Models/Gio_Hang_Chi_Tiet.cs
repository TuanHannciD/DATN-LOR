#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class Gio_Hang_Chi_Tiet
    {
        [Key]
        public Guid ID_Gio_Hang_Chi_Tiet { get; set; }

        [Required]
        public Guid ID_Gio_Hang { get; set; }

        [Required]
        public Guid ID_Spct { get; set; }

        public Guid? Ma_Km { get; set; }

        [Required]
        public float SoLuong { get; set; }

        [Required]
        public float Gia { get; set; }

        [ForeignKey("ID_Gio_Hang")]
        public virtual Gio_Hang Gio_Hang { get; set; }

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }

        [ForeignKey("Ma_Km")]
        public virtual KhuyenMai? KhuyenMai { get; set; }
    }
} 