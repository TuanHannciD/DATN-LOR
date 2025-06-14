#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class Admin
    {
        [Key]
        [StringLength(30)]
        public required string HoTenAdmin { get; set; }

        [Required]
        [StringLength(30)]
        public required string AnhDaiDien { get; set; }

        public DateTime? NgaySinh { get; set; }

        [Required]
        [StringLength(30)]
        public required string Email { get; set; }

        [Required]
        [StringLength(10)]
        public required string Sdt { get; set; }

        [Required]
        [StringLength(30)]
        public required string DiaChi { get; set; }

        [Required]
        [StringLength(30)]
        public required string UserName { get; set; }

        [ForeignKey("UserName")]
        public virtual User User { get; set; }
    }
} 