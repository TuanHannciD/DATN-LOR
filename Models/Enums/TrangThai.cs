using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.Enums
{
    public enum TrangThai
    {
        [Display(Name="Còn Hàng")]
        Conhang = 0,
        [Display(Name = "Hết Hàng")]
        HetHang = 1
    }
}