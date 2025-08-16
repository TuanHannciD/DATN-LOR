using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;
using AuthDemo.Common;

namespace AuthDemo.Areas.Admin.Services
{
    public class ChiTietGiayService : IChiTietGiayService
    {
        private readonly ApplicationDbContext _db;
        public ChiTietGiayService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ChiTietGiay> GetAll()
        {
            try
            {
                return [.. _db.ChiTietGiays];

            }
            catch (Exception ex)
            {
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
        public void Add(ChiTietGiay entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                _db.ChiTietGiays.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết giày: " + ex.Message, ex);
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


        public void Delete(Guid id)
        {
            try
            {
                var obj = _db.ChiTietGiays.Find(id);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy chi tiết giày để xóa!");
                obj.IsDelete = true;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết giày: " + ex.Message, ex);
            }
        }
        public IEnumerable<ChiTietGiayVM.IndexVM> GetAllIndexVM()
        {
            return _db.ChiTietGiays.Select(ct => new ChiTietGiayVM.IndexVM
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
            }).ToList();
        }
    }
}
