using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Services
{
    public class KichThuocService : IKichThuocService
    {
        private readonly ApplicationDbContext _db;
        public KichThuocService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<IEnumerable<KichThuoc>>> GetAll()
        {
            try
            {
                var data = await _db.KichThuocs
                    .Where(ct => !ct.IsDelete)
                    .ToListAsync();
                return ApiResponse<IEnumerable<KichThuoc>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<KichThuoc>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public KichThuoc? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.KichThuocs.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy kích thước theo ID: " + ex.Message, ex);
            }
        }
        public void Add(KichThuoc entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                _db.KichThuocs.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm kích thước: " + ex.Message, ex);
            }
        }
        public void Update(KichThuoc entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                var obj = _db.KichThuocs.Find(entity.SizeID);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy kích thước để cập nhật!");
                _db.Entry(obj).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật kích thước: " + ex.Message, ex);
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
                var obj = _db.KichThuocs.Find(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy kích thước đang xóa");
                obj.IsDelete = true;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Xóa kích thước thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
    }
}
