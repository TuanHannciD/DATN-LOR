#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class Don_Hang_Thanh_Toan
    {
        [Key]
        public Guid ID_Don_Hang_Thanh_Toan { get; set; }

        [Required]
        public Guid ID_ThanhToan { get; set; }

        [Required]
        public Guid ID_Don_Hang { get; set; }

        [Required]
        [StringLength(30)]
        public required string Status { get; set; }

        public DateTime? NgayTT { get; set; }

        [Required]
        [StringLength(50)]
        public required string KieuTT { get; set; }

        [ForeignKey("ID_ThanhToan")]
        public virtual ThanhToan ThanhToan { get; set; }

        [ForeignKey("ID_Don_Hang")]
        public virtual DonHang DonHang { get; set; }
    }
} 