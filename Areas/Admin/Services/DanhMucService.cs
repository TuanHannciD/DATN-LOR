using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using AuthDemo.Data;
using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Services
{
    public class DanhMucService : IDanhMucService
    {
        private readonly ApplicationDbContext _db;
        public DanhMucService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ApiResponse<CreateDanhMuc>> AddAsync(CreateDanhMuc createDanhMuc)
        {
            var name = _db.DanhMucs.Any(c => c.TenDanhMuc == createDanhMuc.Ten);
            if (name == true)
            {
                return ApiResponse<CreateDanhMuc>.FailResponse("error", $"Tên danh mục {createDanhMuc.Ten} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(createDanhMuc.Ten))
                return ApiResponse<CreateDanhMuc>.FailResponse("error", "Tên danh mục không được để trống");

            string[] words = createDanhMuc.Ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            //Trùng code 
            while (await _db.DanhMucs.AnyAsync(c => c.MaDanhMucCode == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }
            var entity = new DanhMuc
            {
                TenDanhMuc = createDanhMuc.Ten,
                MaDanhMucCode = finalCode,
                MoTa = createDanhMuc.MoTa
            };

            try
            {
                await _db.DanhMucs.AddAsync(entity);
                await _db.SaveChangesAsync();
                return ApiResponse<CreateDanhMuc>.SuccessResponse(createDanhMuc, "Thêm danh mục thành công");

            }
            catch (Exception ex)
            {
                return ApiResponse<CreateDanhMuc>.FailResponse("Error", $"Lỗi khi thêm danh mục: {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> Delete(Guid id)
        {
            try
            {
                var obj = await _db.DanhMucs
                   .Include(th => th.ChiTietGiays)
                   .FirstOrDefaultAsync(th => th.CategoryID == id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy danh mục đang xóa");
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
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa danh mục");
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
                var obj = await _db.DanhMucs.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy danh mục đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục danh mục thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }

        public async Task<ApiResponse<IEnumerable<DanhMuc>>> GetAll()
        {
            try
            {
                var data = await _db.DanhMucs.Where(ct => !ct.IsDelete).ToListAsync();
                return ApiResponse<IEnumerable<DanhMuc>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<DanhMuc>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }

        }
        public async Task<ApiResponse<IEnumerable<DanhMuc>>> GetAllDelete()
        {
            try
            {
                var data = await _db.DanhMucs
                    .Where(ct => ct.IsDelete)
                    .ToListAsync();
                return ApiResponse<IEnumerable<DanhMuc>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<DanhMuc>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }

        public DanhMuc? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.DanhMucs.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh mục theo ID: " + ex.Message, ex);
            }
        }

        public void Update(DanhMuc entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                var obj = _db.DanhMucs.Find(entity.CategoryID);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy danh mục để cập nhật!");
                _db.Entry(obj).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật danh mục: " + ex.Message, ex);
            }
        }
    }
}
