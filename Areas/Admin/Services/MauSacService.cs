using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Services
{
    public class MauSacService : IMauSacService
    {
        private readonly ApplicationDbContext _db;
        public MauSacService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<IEnumerable<MauSac>>> GetAll()
        {
            try
            {
                var data = await _db.MauSacs
                .Where(ct => !ct.IsDelete)
                .ToListAsync();
                return ApiResponse<IEnumerable<MauSac>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MauSac>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<MauSac>>> GetAllDelete()
        {
            try
            {
                var data = await _db.MauSacs
                    .Where(ct => ct.IsDelete)
                    .ToListAsync();
                return ApiResponse<IEnumerable<MauSac>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<MauSac>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public MauSac? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.MauSacs.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy màu sắc theo ID: " + ex.Message, ex);
            }
        }
        public async Task<ApiResponse<CreateMauSac>> AddAsync(CreateMauSac create)
        {
            var name = _db.MauSacs.Any(c => c.TenMau == create.Ten);
            if (name == true)
            {
                return ApiResponse<CreateMauSac>.FailResponse("error", $"Tên màu sắc {create.Ten} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(create.Ten))
                return ApiResponse<CreateMauSac>.FailResponse("EmptyName", "Tên màu sắc không được để trống");

            string[] words = create.Ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            while (await _db.MauSacs.AnyAsync(g => g.MaMau == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }

            var entity = new MauSac
            {
                TenMau = create.Ten,
                MaMau = finalCode,
            };

            try
            {
                await _db.MauSacs.AddAsync(entity);
                await _db.SaveChangesAsync();

                var result = new CreateMauSac
                {
                    Ten = entity.TenMau
                };

                return ApiResponse<CreateMauSac>.SuccessResponse(result, "Thêm màu sắc thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<CreateMauSac>.FailResponse("AddError", $"Lỗi khi thêm màu sắc: {ex.Message}");
            }
        }
        public async Task<ApiResponse<string>> Update(MauSac entity)
        {
            if (entity == null) return ApiResponse<string>.FailResponse("Entity_Null", "Lỗi dữ liệu gửi về kiểm tra lại");
            try
            {
                var obj = _db.MauSacs.Find(entity.ColorID);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy kích thước cần cập nhật");
                if (obj.IsDelete) return ApiResponse<string>.FailResponse("Already_Deleted", "Sản phẩm đã bị xóa trước đó vui lòng kiểm tra lại");

                var isDuplicate = await _db.MauSacs.AnyAsync(c => c.TenMau == entity.TenMau && c.ColorID != entity.ColorID);
                bool isDuplicateID = await _db.MauSacs.AnyAsync(c => c.MaMau == entity.MaMau && c.ColorID != entity.ColorID);


                if (isDuplicate) return ApiResponse<string>.FailResponse("Duplicate_Name", "Tên màu sắc bị trùng vui lòng kiểm tra lại ");
                if (isDuplicateID) return ApiResponse<string>.FailResponse("Duplicate_Code", "Mã màu sắc đã tồn tại");


                _db.Entry(obj).CurrentValues.SetValues(entity);
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Update_Success", "Cập nhật màu sắc thành công");
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
                var obj = await _db.MauSacs
                   .Include(th => th.ChiTietGiays)
                   .FirstOrDefaultAsync(th => th.ColorID == id);
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
                var obj = await _db.MauSacs.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy màu sắc đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục màu sắc thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }
    }


}
