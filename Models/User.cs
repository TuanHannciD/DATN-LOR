#nullable disable
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class User
    {
        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(30)]
        public string Role { get; set; }
    }
} 