#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class AnhSp
    {
        [Key]
        public Guid ID_AnhSp { get; set; }

        [Required]
        [StringLength(30)]
        public required string FileAnh { get; set; }

        public Guid? ID_Spct { get; set; }

        [ForeignKey("ID_Spct")]
        public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
    }
} 