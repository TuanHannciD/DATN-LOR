using System.Collections.Generic;
using AuthDemo.Models.ViewModels;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IBanHangTaiQuayService
    {
        List<BanHangTaiQuayVM> SearchSanPham(string keyword);
    }
} 