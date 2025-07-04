#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class GiaoHang
    {
        [Key]
        public Guid ID_GiaoHang { get; set; }

        [Required]
        public Guid ID_ThanhToan { get; set; }

        public Guid? ID_Don_Hang { get; set; }

        public DateTime NgayPhanCongGiaoHang { get; set; }

        [Required]
        public DateTime ThoiGianDuKienGiaoHang { get; set; }

        public DateTime? ThoiGianThucTeGiaoHang { get; set; }

        [Required]
        [StringLength(50)]
        public required string TrangThaiGiaoHang { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime NgayCapNhap { get; set; }

        [ForeignKey("ID_ThanhToan")]
        public virtual ThanhToan ThanhToan { get; set; }

        [ForeignKey("ID_Don_Hang")]
        public virtual DonHang? DonHang { get; set; }
    }
} 