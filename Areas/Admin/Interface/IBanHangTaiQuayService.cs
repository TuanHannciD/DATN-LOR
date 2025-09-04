using System.Collections.Generic;
using AuthDemo.Models.ViewModels;
using System;
using AuthDemo.Models;
using AuthDemo.Common;
using CloudinaryDotNet;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IBanHangTaiQuayService
    {
        List<BanHangTaiQuayVM> SearchSanPham(string keyword);
        Task<ApiResponse<QuickAddCustomerVM>> CreateKhachHang(QuickAddCustomerVM model);
        List<KhachHangDropdownVM> SearchKhachHang(string keyword);
        List<CartItemDisplayVM> GetCartItems(string tenDangNhap);
        ApiResponse<string> UpdateCart(string tenDangNhap, Guid shoeDetailId, string actionType);
        void UpdateDiscountCartItem(Guid cartDetailId, decimal? chietKhauPhanTram, decimal? chietKhauTienMat, bool? isTangKem, string reason);
    }
}
