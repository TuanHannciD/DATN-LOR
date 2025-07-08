using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class ChiTietGiay : ThongTinChung
    {
        [Key]
        public Guid ShoeDetailID { get; set; } // giữ nguyên
        [Required]
        public Guid ShoeID { get; set; } // giữ nguyên
        [Required]
        public Guid CategoryID { get; set; } // giữ nguyên
        [Required]
        public Guid SizeID { get; set; } // giữ nguyên
        [Required]
        public Guid ColorID { get; set; } // giữ nguyên
        [Required]
        public Guid MaterialID { get; set; } // giữ nguyên
        [Required]
        public Guid BrandID { get; set; } // giữ nguyên
        public int SoLuong { get; set; }
        public decimal Gia { get; set; }
    }
} 