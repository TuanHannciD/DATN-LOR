using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class DanhMuc : ThongTinChung
    {
        [Key]
        public Guid CategoryID { get; set; } // giữ nguyên
        [StringLength(50)]
        public string TenDanhMuc { get; set; }
        [StringLength(50)]
        public string MaDanhMucCode { get; set; }
        [StringLength(255)]
        public string MoTa { get; set; }
        public Guid? ParentID { get; set; } // giữ nguyên
    }
} 