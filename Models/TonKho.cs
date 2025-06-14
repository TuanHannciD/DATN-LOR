using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class TonKho
    {
        [Key]
        public Guid ID_TonKho { get; set; }

        [Required]
        public Guid ID_Spct { get; set; }

        [Required]
        public int SoLuongTonKho { get; set; }

        public DateTime NgayCapNhap { get; set; } = DateTime.Now;

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet SanPhamChiTiet { get; set; }
    }
} 