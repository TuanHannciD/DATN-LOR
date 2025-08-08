using System.Collections.Generic;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IHoaDonService
    {
        List<GetAllHoaDonVM> GetAllHoaDon();
        HoaDonDTO GetHoaDonById(int id);
        Task<Result<HoaDonDTO>> CreateHoaDon(CreateHoaDonVM createHoaDonVM, string tenDangNhap);
        // Tính tổng tiền của hóa đơn
        HoaDonTongTienVM TinhTienHoaDon(Guid cartID, decimal? giamGiaPhanTram, decimal? giamGiaTienMat);
        
    }
}