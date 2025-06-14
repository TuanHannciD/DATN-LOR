#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class HangSX
    {
        [Key]
        public Guid ID_Hang { get; set; }

        [Required]
        [StringLength(30)]
        public string HangSXName { get; set; }
    }
} 