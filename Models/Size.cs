#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class Size
    {
        [Key]
        public Guid ID_Size { get; set; }

        [Required]
        [StringLength(30)]
        public string SizeName { get; set; }
    }
} 