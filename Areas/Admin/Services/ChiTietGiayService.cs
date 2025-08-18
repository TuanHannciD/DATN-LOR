using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Services
{
    public class ChiTietGiayService : IChiTietGiayService
    {
        private readonly ApplicationDbContext _db;
        public ChiTietGiayService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ChiTietGiay> GetAll()
        {
            try
            {
                // Kiểm tra DbSet có null không, tránh lỗi ArgumentNullException
                var query = _db.ChiTietGiays ?? Enumerable.Empty<ChiTietGiay>();

                // Lọc các bản ghi chưa bị xóa
                var result = query
                    .Where(ct => !ct.IsDelete)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                // Ném lại exception với thông báo rõ ràng
                throw new Exception("Lỗi khi lấy danh sách chi tiết giày: " + ex.Message, ex);
            }
        }

        public ChiTietGiay? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.ChiTietGiays.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết giày theo ID: " + ex.Message, ex);
            }
        }
        public ApiResponse<string> Update(EditVM entity)
        {
            if (entity.ShoeDetailID == Guid.Empty)
                return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "ID chi tiết giày không hợp lệ!");

            var existingDetail = _db.ChiTietGiays.Find(entity.ShoeDetailID);
            if (existingDetail == null)
                return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy chi tiết giày để cập nhật.");

            // Cập nhật các trường
            existingDetail.ShoeID = entity.ShoeID;
            existingDetail.SizeID = entity.SizeID;
            existingDetail.ColorID = entity.ColorID;
            existingDetail.MaterialID = entity.MaterialID;
            existingDetail.BrandID = entity.BrandID;
            existingDetail.CategoryID = entity.CategoryID;
            existingDetail.SoLuong = entity.SoLuong;
            existingDetail.Gia = entity.Gia;

            _db.SaveChanges();

            return ApiResponse<string>.SuccessResponse("Cập nhật chi tiết giày thành công.");
        }


        public ApiResponse<string> Delete(Guid id)
        {
            try
            {
                var obj = _db.ChiTietGiays.Find(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy đang xóa");

                obj.IsDelete = true;
                _db.SaveChanges();
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa chi tiết giày");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<IndexVM>>> GetAllIndexVMAsync()
        {
            try
            {
                var data = await _db.ChiTietGiays
                    .Where(ct => !ct.IsDelete)
                    .Select(ct => new IndexVM
                    {
                        ShoeDetailID = ct.ShoeDetailID,
                        TenGiay = ct.Giay != null ? ct.Giay.TenGiay : "Chưa có",
                        TenKichThuoc = ct.KichThuoc != null ? ct.KichThuoc.TenKichThuoc : "Chưa có",
                        TenMau = ct.MauSac != null ? ct.MauSac.TenMau : "Chưa có",
                        TenChatLieu = ct.ChatLieu != null ? ct.ChatLieu.TenChatLieu : "Chưa có",
                        TenThuongHieu = ct.ThuongHieu != null ? ct.ThuongHieu.TenThuongHieu : "Chưa có",
                        TenDanhMuc = ct.DanhMuc != null ? ct.DanhMuc.TenDanhMuc : "Chưa có",
                        SoLuong = ct.SoLuong,
                        Gia = ct.Gia
                    })
                    .ToListAsync();

                return ApiResponse<IEnumerable<IndexVM>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<IndexVM>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }

        public ApiResponse<EditVM> Add(EditVM editVM)
        {
            throw new NotImplementedException();
            // if (editVM == null)
            // {
            //     return ApiResponse<EditVM>.FailResponse("Entity_null", "Lỗi dữ liệu gửi đến server null");
            // }
            // if (editVM.SoLuong < 0)
            // {
            //     return ApiResponse<EditVM>.FailResponse("Quantity_Exceeded", "Số lượng không được bé hơn 0");
            // }
            // if (editVM.Gia < 1000)
            // {
            //     return ApiResponse<EditVM>.FailResponse("Price_Fail", "Giá phải lớn hơn 100");
            // }
            // var entity = new EditVM
            // {
            //     ShoeDetailID = editVM.ShoeDetailID,
            //     BrandID = editVM.BrandID,
            //     CategoryID = editVM.CategoryID,
            //     ColorID = editVM.ColorID,
            //     Gia = editVM.Gia,

            // };
        }
    }
}
