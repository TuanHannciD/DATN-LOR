using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IHoaDonService
    {
        List<dynamic> GetTrangThaiList();
        List<GetAllHoaDonVM> GetAllHoaDon();
        HoaDonDTO GetHoaDonById(int id);
        Task<Result<HoaDonDTO>> CreateHoaDon(CreateHoaDonVM createHoaDonVM, string tenDangNhap);
        // Tính tổng tiền của hóa đơn
        HoaDonTongTienVM TinhTienHoaDon(Guid cartID, decimal? giamGiaPhanTram, decimal? giamGiaTienMat);
        // Cập nhật trạng thái thanh toán của hóa đơn
        Task<Result<string>> XacnhanTienMat(bool confirmdone, Guid orderId);
        Task<ApiResponse<string>> UpdateTrangThai(Guid HoaDonID, string TrangThai);
    }
}
