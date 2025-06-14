#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class SanPham
    {
        [Key]
        public Guid ID_Sp { get; set; }

        [Required]
        [StringLength(50)]
        public string Ten_Sp { get; set; }

        [Required]
        public int SoLuongTong { get; set; }

        [Required]
        [StringLength(255)]
        public string MoTa { get; set; }

        [Required]
        [StringLength(255)]
        public string TrangThai { get; set; }
    }
} 