using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Common;
using static AuthDemo.Models.ViewModels.GiayVM;
using AuthDemo.Models.Enums;

namespace AuthDemo.Areas.Admin.Services
{
    public class GiayService : IGiayService
    {
        private readonly ApplicationDbContext _db;
        public GiayService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Giay> GetAll()
        {
            try
            {
                return _db.Giays
                    .Where(g => !g.IsDelete)   // chỉ lấy chưa xóa
                    .AsNoTracking()            // tối ưu khi chỉ đọc
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách sản phẩm: " + ex.Message, ex);
            }
        }

        public IEnumerable<Giay> GetAllDelete()
        {
            try
            {
                return _db.Giays
                    .Where(g => g.IsDelete)   // chỉ lấy chưa xóa
                    .AsNoTracking()            // tối ưu khi chỉ đọc
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách sản phẩm: " + ex.Message, ex);
            }
        }
        public Giay? GetById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("ID không hợp lệ!");
            try
            {
                return _db.Giays.Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy sản phẩm theo ID: " + ex.Message, ex);
            }
        }
        public async Task<ApiResponse<GiayCreate>> AddAsync(GiayCreate model)
        {
            var name = _db.Giays.Any(c => c.TenGiay == model.TenGiay);
            if (name == true)
            {
                return ApiResponse<GiayCreate>.FailResponse("error", $"Tên giày {model.TenGiay} đã có rồi. Vui lòng tạo tên khác");

            }
            if (string.IsNullOrWhiteSpace(model.TenGiay))
                return ApiResponse<GiayCreate>.FailResponse("AddError", "Tên Giày không được để trống");

            // Sinh mã giày từ tên
            string[] words = model.TenGiay.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string baseCode = string.Concat(words.Select(w => char.ToUpper(w[0])));
            string finalCode = baseCode;
            int counter = 1;

            // Kiểm tra trùng trong DB và tăng số nếu cần
            while (await _db.Giays.AnyAsync(g => g.MaGiayCode == finalCode))
            {
                counter++;
                finalCode = baseCode + counter;
            }

            var entity = new Giay
            {
                TenGiay = model.TenGiay,
                MaGiayCode = finalCode,
                TrangThai = model.TrangThai ? TrangThai.Conhang : TrangThai.HetHang,
                MoTa = model.MoTa
            };

            try
            {
                await _db.Giays.AddAsync(entity);
                await _db.SaveChangesAsync();
                var result = new GiayCreate
                {
                    TenGiay = entity.TenGiay
                };
                return ApiResponse<GiayCreate>.SuccessResponse(result, "Thêm giày thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<GiayCreate>.FailResponse("AddError", $"Lỗi khi thêm sản phẩm: {ex.Message}");
            }
        }

        public void Update(Giay entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                var obj = _db.Giays.Find(entity.ShoeID);
                ArgumentNullException.ThrowIfNull(obj, "Không tìm thấy sản phẩm để cập nhật!");
                _db.Entry(obj).CurrentValues.SetValues(entity);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật sản phẩm: " + ex.Message, ex);
            }
        }
        public async Task<ApiResponse<string>> Delete(Guid id)
        {
            try
            {
                var obj = await _db.Giays.FindAsync(id);
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
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa chi tiết giày");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
        public IEnumerable<GiayFullInfoVM> GetGiayFullInfoList()
        {
            var giayList = _db.Giays
                .Select(g => new GiayFullInfoVM
                {
                    Giay = g,
                    MaGiayCode = g.MaGiayCode,
                    MoTa = g.MoTa,
                    ChiTietGiays = _db.ChiTietGiays
                        .Where(ct => ct.ShoeID == g.ShoeID)
                        .Select(ct => new ChiTietGiay
                        {
                            ShoeDetailID = ct.ShoeDetailID,
                            ShoeID = ct.ShoeID,
                            SizeID = ct.SizeID,
                            ColorID = ct.ColorID,
                            MaterialID = ct.MaterialID,
                            BrandID = ct.BrandID,
                            CategoryID = ct.CategoryID,
                            SoLuong = ct.SoLuong,
                            Gia = ct.Gia,
                            KichThuoc = ct.KichThuoc,
                            MauSac = ct.MauSac,
                            ChatLieu = ct.ChatLieu,
                            ThuongHieu = ct.ThuongHieu
                        }).ToList(),
                    TenDanhMuc = _db.ChiTietGiays
                        .Where(ct => ct.ShoeID == g.ShoeID)
                        .Select(ct => ct.DanhMuc != null ? ct.DanhMuc.TenDanhMuc : null)
                        .FirstOrDefault() ?? "Chưa có",
                    TenThuongHieu = _db.ChiTietGiays
                        .Where(ct => ct.ShoeID == g.ShoeID)
                        .Select(ct => ct.ThuongHieu != null ? ct.ThuongHieu.TenThuongHieu : null)
                        .FirstOrDefault() ?? "Chưa có",
                    TenChatLieu = _db.ChiTietGiays
                        .Where(ct => ct.ShoeID == g.ShoeID)
                        .Select(ct => ct.ChatLieu != null ? ct.ChatLieu.TenChatLieu : null)
                        .FirstOrDefault() ?? "Chưa có"
                }).ToList();
            return giayList;
        }

        public async Task<ApiResponse<string>> Restore(Guid id)
        {
            try
            {
                var obj = await _db.Giays.FindAsync(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy đang khôi phục");
                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục giày thành công");


            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi khôi phục:" + ex.Message);
            }
        }
    }
}
