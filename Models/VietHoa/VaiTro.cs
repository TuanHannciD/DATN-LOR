using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.VietHoa
{
    public class VaiTro : ThongTinChung
    {
        [Key]
        public Guid RoleID { get; set; } // giữ nguyên
        [Required]
        [StringLength(50)]
        public string TenVaiTro { get; set; }
        [StringLength(255)]
        public string MoTa { get; set; }
    }
} 