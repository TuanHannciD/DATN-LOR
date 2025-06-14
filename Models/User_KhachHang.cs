using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class User_KhachHang
    {
        [Key]
        public Guid ID_User { get; set; }

        [Required]
        [StringLength(30)]
        public required string HoTen { get; set; }

        [Required]
        [StringLength(30)]
        public required string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        // Navigation properties
        [ForeignKey("UserName")]
        public virtual User User { get; set; }

        // Khóa ngoại tổng hợp đến KhachHang sẽ được cấu hình trong DbContext
        public virtual KhachHang KhachHang { get; set; }
    }
} 