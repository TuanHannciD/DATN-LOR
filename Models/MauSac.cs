#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class MauSac
    {
        [Key]
        public Guid ID_MauSac { get; set; }

        [Required]
        [StringLength(30)]
        public string MauSacName { get; set; }
    }
} 