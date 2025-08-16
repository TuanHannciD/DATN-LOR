using System.ComponentModel.DataAnnotations;
using AuthDemo.Models.Enums;

namespace AuthDemo.Models
{
    public class Giay : ThongTinChung
    {
        [Key]
        public Guid ShoeID { get; set; } // giữ nguyên
        [StringLength(100)]
        public string TenGiay { get; set; }
        [StringLength(50)]
        public string MaGiayCode { get; set; }
        [StringLength(255)]
        public string MoTa { get; set; }
        public string? AnhDaiDien { get; set; } // ImageURL
        public TrangThai TrangThai { get; set; }
        public ICollection<ChiTietGiay>? ChiTietGiays { get; set; }

    }
}
