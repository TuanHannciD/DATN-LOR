using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Common;
using AuthDemo.Data;
using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;
using static AuthDemo.Models.ViewModels.VMCHUNG;

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
        public async Task<ApiResponse<IEnumerable<ThuongHieu>>> GetAllDelete()
        {
            try
            {
                var data = await _db.ThuongHieus
                    .Where(ct => ct.IsDelete)
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
        public async Task<ApiResponse<CreateHangSanXuat>> AddAsync(CreateHangSanXuat create)
        {
            var name = _db.ThuongHieus.Any(c => c.TenThuongHieu == create.Ten);
            if (name == true)
            {
                return ApiResponse<CreateHangSanXuat>.FailResponse("error", $"Tên thương hiệu {create.Ten} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(create.Ten))
                return ApiResponse<CreateHangSanXuat>.FailResponse("error", "Lỗi khi thêm hãng không được bỏ trống");
            // Sinh mã giày từ tên
            string[] words = create.Ten.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            while (await _db.ThuongHieus.AnyAsync(g => g.MaThuongHieuCode == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }

            var entity = new ThuongHieu
            {
                TenThuongHieu = create.Ten,
                MaThuongHieuCode = finalCode,
            };

            try
            {
                await _db.ThuongHieus.AddAsync(entity);
                await _db.SaveChangesAsync();

                // Trả về thông tin giày mới dưới dạng GiayCreate
                var result = new CreateHangSanXuat
                {
                    Ten = entity.TenThuongHieu
                    // Nếu muốn, thêm các trường khác mà GiayCreate có
                };

                return ApiResponse<CreateHangSanXuat>.SuccessResponse(result, "Thêm giày thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<CreateHangSanXuat>.FailResponse("AddError", $"Lỗi khi thêm sản phẩm: {ex.Message}");
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

                // Xóa tất cả chi tiết giày liên quan
                if (obj.ChiTietGiays != null && obj.ChiTietGiays.Count > 0)
                {
                    foreach (var ct in obj.ChiTietGiays)
                    {
                        ct.IsDelete = true;
                    }
                }
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Xóa thương hiệu thành công");

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
                var obj = await _db.ThuongHieus.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_Not_Found", "Không tìm thấy thương hiệu đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục thương hiệu thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }

        
    }
}
