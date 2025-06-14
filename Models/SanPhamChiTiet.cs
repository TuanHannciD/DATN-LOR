#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class SanPhamChiTiet
    {
        [Key]
        public Guid ID_Spct { get; set; }

        [Required]
        public Guid ID_Sp { get; set; }

        [Required]
        [StringLength(30)]
        public required string TenSp { get; set; }

        [Required]
        [StringLength(500)]
        public required string MoTa { get; set; }

        [Required]
        public int SoLuongBan { get; set; }

        [Required]
        public float Gia { get; set; }

        [Required]
        [StringLength(30)]
        public required string AnhDaiDien { get; set; }

        [StringLength(30)]
        public string? DanhGia { get; set; }

        public Guid? ID_ChatLieu { get; set; }

        public Guid? ID_Hang { get; set; }

        public Guid? ID_MauSac { get; set; }

        public Guid? ID_Size { get; set; }

        [ForeignKey("ID_Sp")]
        public virtual SanPham SanPham { get; set; }

        [ForeignKey("ID_ChatLieu")]
        public virtual ChatLieu? ChatLieu { get; set; }

        [ForeignKey("ID_Hang")]
        public virtual HangSX? HangSX { get; set; }

        [ForeignKey("ID_MauSac")]
        public virtual MauSac? MauSac { get; set; }

        [ForeignKey("ID_Size")]
        public virtual Size? Size { get; set; }
    }
} 