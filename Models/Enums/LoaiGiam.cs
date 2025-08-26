using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.Enums
{
    public enum LoaiGiam
    {
        [Display(Name = "Phần Trăm")]
        PhanTram = 0,
        [Display(Name = "Tiền")]
        Tien = 1
    }
}
