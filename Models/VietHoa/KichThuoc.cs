using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class KichThuoc : ThongTinChung
    {
        [Key]
        public Guid SizeID { get; set; } // giữ nguyên
        [StringLength(50)]
        public string TenKichThuoc { get; set; }
        [StringLength(50)]
        public string MaKichThuocCode { get; set; }
    }
} 