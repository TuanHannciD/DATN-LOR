using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Services
{
    public class ChatLieuService : IChatLieuService
    {
        private readonly ApplicationDbContext _db;
        public ChatLieuService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<IEnumerable<ChatLieu>>> GetAll()
        {
            try
            {
                var data = await _db.ChatLieus
                    .Where(ct => !ct.IsDelete)
                    .ToListAsync();
                return ApiResponse<IEnumerable<ChatLieu>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ChatLieu>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public ChatLieu? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.ChatLieus.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chất liệu theo ID: " + ex.Message, ex);
            }
        }
        public void Add(ChatLieu entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                _db.ChatLieus.Add(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chất liệu: " + ex.Message, ex);
            }
        }
        public void Update(ChatLieu entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                var obj = _db.ChatLieus.Find(entity.MaterialID);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy chất liệu để cập nhật!");
                _db.Entry(obj).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chất liệu: " + ex.Message, ex);
            }
        }
        public ApiResponse<string> Delete(Guid id)
        {
            try
            {
                var obj = _db.ChatLieus.Find(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy đang xóa");
                obj.IsDelete = true;
                _db.SaveChanges();
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa chi chất liệu");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
    }
} 
