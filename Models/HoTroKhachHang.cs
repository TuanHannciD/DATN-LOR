#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class HoTroKhachHang
    {
        [Key]
        public Guid ID_HoTroKhachHang { get; set; }

        [Required]
        public Guid MaNV { get; set; }

        [Required]
        public Guid ID_User { get; set; }

        [Required]
        [StringLength(50)]
        public required string LoaiHT { get; set; }

        [Required]
        [StringLength(50)]
        public required string PTLienLac { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }

        [ForeignKey("ID_User")]
        public virtual User_KhachHang User_KhachHang { get; set; }
    }
} 