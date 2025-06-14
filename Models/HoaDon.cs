#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class HoaDon
    {
        [Key]
        public Guid ID_HoaDon { get; set; }

        [Required]
        public Guid ID_User { get; set; }

        [ForeignKey("ID_User")]
        public virtual User_KhachHang User_KhachHang { get; set; }
    }
} 