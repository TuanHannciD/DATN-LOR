using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class VaiTroNguoiDung
    {
        [Key]
        public Guid UserRoleID { get; set; } // giữ nguyên
        [Required]
        public Guid UserID { get; set; } // giữ nguyên
        [Required]
        public Guid RoleID { get; set; } // giữ nguyên
    }
} 