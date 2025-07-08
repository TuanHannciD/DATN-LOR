using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class ThuongHieu : ThongTinChung
    {
        [Key]
        public Guid BrandID { get; set; } // giữ nguyên
        [StringLength(50)]
        public string TenThuongHieu { get; set; }
        [StringLength(50)]
        public string MaThuongHieuCode { get; set; }
    }
} 