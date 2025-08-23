using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.Enums
{
    public enum PhuongThucThanhToan
    {
        [Display(Name = "Thanh toán khi nhận")]
        TienMat = 0,
        [Display(Name = "Chuyển khoản")]
        ChuyenKhoan = 1,
        [Display(Name = "Thẻ tín dụng")]
        TheTinDung = 2,
        [Display(Name = "Ví điện tử")]
        ViDienTu = 3
    }
} 