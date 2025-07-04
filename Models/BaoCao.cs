#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class BaoCao
    {
        [Key]
        public Guid ID_BaoCao { get; set; }

        [Required]
        public DateTime NgayBaoCao { get; set; }

        [Required]
        [StringLength(50)]
        public required string LoaiBaoCao { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DoanhThu { get; set; }

        public int? SoLuongHangBanRa { get; set; }

        public int? SoLuongHangTon { get; set; }

        public int? TongSoDonHang { get; set; }

        public int? SoLuongDonHangHoanThanh { get; set; }

        public int? SoLuongDonHangDangXuLy { get; set; }

        public int? SoLuongDonHangBiHuy { get; set; }

        public int? TongSoKhachHang { get; set; }

        public int? SoKhachHangMoi { get; set; }

        public int? SoKhachHangTroLai { get; set; }

        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhap { get; set; }

        [Required]
        [StringLength(30)]
        public required string HoTenAdmin { get; set; }

        [ForeignKey("HoTenAdmin")]
        public virtual Admin Admin { get; set; }
    }
} 