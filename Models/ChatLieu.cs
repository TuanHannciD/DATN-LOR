#nullable disable
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class ChatLieu
    {
        [Key]
        public Guid ID_ChatLieu { get; set; }

        [Required]
        [StringLength(30)]
        public string ChatLieuName { get; set; }
    }
} 