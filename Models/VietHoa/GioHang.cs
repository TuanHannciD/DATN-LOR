using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class GioHang : ThongTinChung
    {
        [Key]
        public Guid CartID { get; set; } // giữ nguyên
        [Required]
        public Guid UserID { get; set; } // giữ nguyên
    }
} 