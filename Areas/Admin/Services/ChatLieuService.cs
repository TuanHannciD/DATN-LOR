using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;
using static AuthDemo.Models.ViewModels.VMCHUNG;

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
                    .Where(ct => !ct.IsDelete).OrderByDescending(h => h.NgayTao) 
                    .ToListAsync();
                return ApiResponse<IEnumerable<ChatLieu>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ChatLieu>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<ChatLieu>>> GetAllDelete()
        {
            try
            {
                var data = await _db.ChatLieus
                    .Where(ct => ct.IsDelete)
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
        public async Task<ApiResponse<CreateChatLieu>> AddAsync(CreateChatLieu createChatLieu)
        {
            var name = _db.ChatLieus.Any(c => c.TenChatLieu == createChatLieu.Ten);
            if (name == true)
            {
                return ApiResponse<CreateChatLieu>.FailResponse("error", $"Tên chất liệu {createChatLieu.Ten} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(createChatLieu.Ten))
                return ApiResponse<CreateChatLieu>.FailResponse("error", "Tên chất liệu không được để trống");

            string[] words = createChatLieu.Ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            //Trùng code 
            while (await _db.ChatLieus.AnyAsync(c => c.MaChatLieuCode == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }
            var entity = new ChatLieu
            {
                TenChatLieu = createChatLieu.Ten,
                MaChatLieuCode = finalCode,
            };

            try
            {
                await _db.ChatLieus.AddAsync(entity);
                await _db.SaveChangesAsync();
                return ApiResponse<CreateChatLieu>.SuccessResponse(createChatLieu, "Thêm chất liệu thành công");

            }
            catch (Exception ex)
            {
                return ApiResponse<CreateChatLieu>.FailResponse("Error", $"Lỗi khi thêm chất liệu: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> Update(ChatLieu entity)
        {
            if (entity == null)
                return ApiResponse<string>.FailResponse("Entity_Null", "Dữ liệu gửi lên bị null");

            if (entity.MaterialID == Guid.Empty)
                return ApiResponse<string>.FailResponse("ID_Invalid", "ID không hợp lệ");

            try
            {
                var obj = await _db.ChatLieus.FindAsync(entity.MaterialID);

                if (obj == null)
                    return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy chất liệu cần cập nhật");

                if (obj.IsDelete)
                    return ApiResponse<string>.FailResponse("Already_Deleted", "Chất liệu này đã bị xóa, không thể cập nhật");

                //checked trùng tên
                bool isDuplicate = await _db.ChatLieus
                    .AnyAsync(c => c.TenChatLieu == entity.TenChatLieu && c.MaterialID != entity.MaterialID);

                bool isDuplicateID = await _db.ChatLieus.AnyAsync(c => c.MaChatLieuCode == entity.MaChatLieuCode && c.MaterialID != entity.MaterialID);

                if (isDuplicate)
                    return ApiResponse<string>.FailResponse("Duplicate_Name", "Tên chất liệu đã tồn tại");

                if (isDuplicateID) return ApiResponse<string>.FailResponse("Duplicate_Code", "Mã chất liệu đã tồn tại");


                // Cập nhật giá trị
                _db.Entry(obj).CurrentValues.SetValues(entity);

                await _db.SaveChangesAsync();

                return ApiResponse<string>.SuccessResponse("Update_Success", "Cập nhật chất liệu thành công");
            }
            catch (DbUpdateException dbEx)
            {
                return ApiResponse<string>.FailResponse("Database_Error", "Lỗi khi cập nhật DB: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Unhandled_Error", "Đã xảy ra lỗi: " + ex.Message);
            }
        }

        public async Task<ApiResponse<string>> Delete(Guid id)
        {
            try
            {
                var obj = await _db.ChatLieus
                   .Include(th => th.ChiTietGiays)
                   .FirstOrDefaultAsync(th => th.MaterialID == id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy đang xóa");
                obj.IsDelete = true;
                // Xóa tất cả chi tiết giày liên quan
                if (obj.ChiTietGiays != null && obj.ChiTietGiays.Count > 0)
                {
                    foreach (var ct in obj.ChiTietGiays)
                    {
                        ct.IsDelete = true;
                    }
                }
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa chất liệu");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
        public async Task<ApiResponse<string>> Restore(Guid id)
        {
            try
            {
                var obj = await _db.ChatLieus.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy chất liệu đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục chất liệu thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }
    }
}
