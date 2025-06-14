#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class DonHang
    {
        [Key]
        public Guid ID_Don_Hang { get; set; }

        [Required]
        public Guid MaNV { get; set; }

        [ForeignKey("MaNV")]
        public virtual NhanVien NhanVien { get; set; }
    }
} 