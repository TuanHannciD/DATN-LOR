using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class AnhGiay : ThongTinChung
    {
        [Key]
        public Guid ImageShoeID { get; set; } // giữ nguyên
        [Required]
        public Guid ShoeDetailID { get; set; } // giữ nguyên
        [StringLength(255)]
        public string DuongDanAnh { get; set; } // ImageURL
    }
} 