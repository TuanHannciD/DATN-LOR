#nullable disable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthDemo.Models
{
    public class NCC
    {
        [Key]
        public Guid Ma_NCC { get; set; }

        [Required]
        [StringLength(30)]
        public required string NameNCC { get; set; }

        [StringLength(30)]
        public string? NameNLH { get; set; }

        [Required]
        [StringLength(20)]
        public required string SDT { get; set; }

        [StringLength(30)]
        public string? Email { get; set; }

        [StringLength(100)]
        public string? DiaChi { get; set; }

        [StringLength(50)]
        public string? ThanhPho { get; set; }

        [StringLength(50)]
        public string? QuocGia { get; set; }

        [StringLength(200)]
        public string? MoTa { get; set; }

        [Required]
        [StringLength(30)]
        public required string TrangThai { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(30)]
        public required string HoTenAdmin { get; set; }

        [ForeignKey("HoTenAdmin")]
        public virtual Admin Admin { get; set; }
    }
} 