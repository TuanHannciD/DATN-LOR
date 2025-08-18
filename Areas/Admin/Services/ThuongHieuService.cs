using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using AuthDemo.Data;
using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Services
{
    public class ThuongHieuService : IThuongHieuService
    {
        private readonly ApplicationDbContext _db;
        public ThuongHieuService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<IEnumerable<ThuongHieu>>> GetAll()
        {
            try
            {
                var data = await _db.ThuongHieus
                    .Where(ct => !ct.IsDelete)
                    .ToListAsync();
                return ApiResponse<IEnumerable<ThuongHieu>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ThuongHieu>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public ThuongHieu? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.ThuongHieus.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thương hiệu theo ID: " + ex.Message, ex);
            }
        }
        public void Add(ThuongHieu entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                _db.ThuongHieus.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm thương hiệu: " + ex.Message, ex);
            }
        }
        public void Update(ThuongHieu entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                var obj = _db.ThuongHieus.Find(entity.BrandID);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy thương hiệu để cập nhật!");
                _db.Entry(obj).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thương hiệu: " + ex.Message, ex);
            }
        }
        public async Task<ApiResponse<string>> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return ApiResponse<string>.FailResponse("ID_Invalid", "ID không hợp lệ!");
                }
                var obj = _db.ThuongHieus.Find(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy thương hiệu đang xóa");
                obj.IsDelete = true;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Xóa thương hiệu thành công");

            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
    }
}
