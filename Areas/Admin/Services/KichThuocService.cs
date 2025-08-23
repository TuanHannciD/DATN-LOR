using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;
using static AuthDemo.Models.ViewModels.VMCHUNG;

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

        public async Task<ApiResponse<IEnumerable<KichThuoc>>> GetAllDelete()
        {
            try
            {
                var data = await _db.KichThuocs
                    .Where(ct => ct.IsDelete)
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
        public async Task<ApiResponse<CreateKichThuoc>> AddAsync(CreateKichThuoc create)
        {
            var name = _db.KichThuocs.Any(c => c.TenKichThuoc == create.Ten);
            if (name == true)
            {
                return ApiResponse<CreateKichThuoc>.FailResponse("error", $"Tên kích thước {create.Ten} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(create.Ten))
                return ApiResponse<CreateKichThuoc>.FailResponse("EmptyName", "Tên kích thước không được để trống");

            // Sinh mã giày từ tên
            string[] words = create.Ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            while (await _db.KichThuocs.AnyAsync(g => g.MaKichThuocCode == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }

            var entity = new KichThuoc
            {
                TenKichThuoc = create.Ten,
                MaKichThuocCode = finalCode,
            };

            try
            {
                await _db.KichThuocs.AddAsync(entity);
                await _db.SaveChangesAsync();


                var result = new CreateKichThuoc
                {
                    Ten = entity.TenKichThuoc
                };

                return ApiResponse<CreateKichThuoc>.SuccessResponse(result, "Thêm kích thước thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<CreateKichThuoc>.FailResponse("AddError", $"Lỗi khi thêm kích thước: {ex.Message}");
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
                // Xóa tất cả chi tiết giày liên quan
                if (obj.ChiTietGiays != null && obj.ChiTietGiays.Count > 0)
                {
                    foreach (var ct in obj.ChiTietGiays)
                    {
                        ct.IsDelete = true;
                    }
                }
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Xóa kích thước thành công");
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
                var obj = await _db.KichThuocs.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy kích thước đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục kích thước thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }
    }
}
