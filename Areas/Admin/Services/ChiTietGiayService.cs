using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;
using AuthDemo.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Areas.Admin.Services
{
    public class ChiTietGiayService : IChiTietGiayService
    {
        private readonly ApplicationDbContext _db;
        public ChiTietGiayService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ChiTietGiay> GetAll()
        {
            try
            {
                // Kiểm tra DbSet có null không, tránh lỗi ArgumentNullException
                var query = _db.ChiTietGiays ?? Enumerable.Empty<ChiTietGiay>();

                // Lọc các bản ghi chưa bị xóa
                var result = query
                    .Where(ct => !ct.IsDelete)
                    .ToList();

                return result;
            }
            catch (Exception ex)
            {
                // Ném lại exception với thông báo rõ ràng
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


        public async Task<ApiResponse<IEnumerable<IndexVM>>> GetAllIndexVMAsync()
        {
            try
            {
                var data = await _db.ChiTietGiays.OrderByDescending(ct => ct.NgayTao) // Sắp xếp mới nhất -> cũ nhất
                    .Where(ct => !ct.IsDelete)
                    .Select(ct => new IndexVM
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
                    })
                    .ToListAsync();
                // Sắp xếp mới nhất -> cũ nhất


                return ApiResponse<IEnumerable<IndexVM>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<IndexVM>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<IndexVM>>> GetAllDelete()
        {
            try
            {

                // Lọc các bản ghi chưa bị xóa
                var data = await _db.ChiTietGiays
                    .Where(ct => ct.IsDelete)
                    .Select(ct => new IndexVM
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
                    })
                    .ToListAsync();

                return ApiResponse<IEnumerable<IndexVM>>.SuccessResponse(data, "Lấy danh sách thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<IndexVM>>.FailResponse("Fail", "Lỗi khi lấy danh sách: " + ex.Message);
            }
        }

        public async Task<ApiResponse<CreateVM>> Add([FromBody] CreateVM createVM)
        {
            // Validation cơ bản
            if (createVM == null)
                return ApiResponse<CreateVM>.FailResponse("Entity_Null", "Dữ liệu gửi đến server null");

            if (createVM.ShoeID == Guid.Empty)
                return ApiResponse<CreateVM>.FailResponse("ShoeID_Invalid", "ShoeID không hợp lệ");

            if (createVM.MaterialID == Guid.Empty)
                return ApiResponse<CreateVM>.FailResponse("MaterialID_Invalid", "Chất liệu không hợp lệ");

            if (createVM.BrandID == Guid.Empty)
                return ApiResponse<CreateVM>.FailResponse("BrandID_Invalid", "Thương hiệu không hợp lệ");

            if (createVM.CategoryID == Guid.Empty)
                return ApiResponse<CreateVM>.FailResponse("CategoryID_Invalid", "Danh mục không hợp lệ");

            if (createVM.ChiTietImages == null || !createVM.ChiTietImages.Any())
                return ApiResponse<CreateVM>.FailResponse("ChiTietImages_Empty", "Chưa chọn size-color hoặc ảnh");

            if (createVM.SoLuong <= 0)
                return ApiResponse<CreateVM>.FailResponse("Quantity_Invalid", "Số lượng phải lớn hơn 0");

            if (createVM.Gia < 1000)
                return ApiResponse<CreateVM>.FailResponse("Price_Invalid", "Giá phải lớn hơn 1000");

            // Check mismatch Brand / Category / Material
            var existingDetails = await _db.ChiTietGiays
                .Include(d => d.ThuongHieu)
                .Include(d => d.DanhMuc)
                .Include(d => d.ChatLieu)
                .Where(ct => ct.ShoeID == createVM.ShoeID)
                .ToListAsync();

            foreach (var detail in existingDetails)
            {
                if (detail.BrandID != createVM.BrandID)
                    return ApiResponse<CreateVM>.FailResponse(
                        "Brand_Mismatch",
                        $"Thương hiệu không khớp (Hiện tại: {detail.ThuongHieu?.TenThuongHieu ?? "Không rõ"})");

                if (detail.CategoryID != createVM.CategoryID)
                    return ApiResponse<CreateVM>.FailResponse(
                        "Category_Mismatch",
                        $"Danh mục không khớp (Hiện tại: {detail.DanhMuc?.TenDanhMuc ?? "Không rõ"})");

                if (detail.MaterialID != createVM.MaterialID)
                    return ApiResponse<CreateVM>.FailResponse(
                        "Material_Mismatch",
                        $"Chất liệu không khớp (Hiện tại: {detail.ChatLieu?.TenChatLieu ?? "Không rõ"})");
            }

            var chiTietList = new List<ChiTietGiay>();
            var anhGiayList = new List<AnhGiay>();

            foreach (var ct in createVM.ChiTietImages)
            {
                var existingDetail = await _db.ChiTietGiays
                    .FirstOrDefaultAsync(d =>
                        d.ShoeID == createVM.ShoeID &&
                        d.BrandID == createVM.BrandID &&
                        d.CategoryID == createVM.CategoryID &&
                        d.MaterialID == createVM.MaterialID &&
                        d.SizeID == ct.SizeID &&
                        d.ColorID == ct.ColorID);

                if (existingDetail != null)
                {
                    // Check bị xóa
                    if (existingDetail.IsDelete)
                    {
                        return ApiResponse<CreateVM>.FailResponse(
                            "Deleted_Detail_Exists",
                            $"Chi tiết giày (SizeID: {ct.SizeID}, ColorID: {ct.ColorID}) đã bị xóa trước đó."
                        );
                    }

                    // Nếu chưa bị xóa -> cộng số lượng
                    existingDetail.SoLuong += createVM.SoLuong;
                    existingDetail.Gia = createVM.Gia;
                    _db.ChiTietGiays.Update(existingDetail);

                    if (ct.Images != null && ct.Images.Any())
                    {
                        anhGiayList.AddRange(ct.Images.Select(url => new AnhGiay
                        {
                            ImageShoeID = Guid.NewGuid(),
                            ShoeDetailID = existingDetail.ShoeDetailID,
                            DuongDanAnh = url
                        }));
                    }
                }
                else
                {
                    var chiTiet = new ChiTietGiay
                    {
                        ShoeDetailID = Guid.NewGuid(),
                        ShoeID = createVM.ShoeID,
                        SizeID = ct.SizeID,
                        ColorID = ct.ColorID,
                        SoLuong = createVM.SoLuong,
                        Gia = createVM.Gia,
                        MaterialID = createVM.MaterialID,
                        BrandID = createVM.BrandID,
                        CategoryID = createVM.CategoryID
                    };

                    chiTietList.Add(chiTiet);

                    if (ct.Images != null && ct.Images.Any())
                    {
                        anhGiayList.AddRange(ct.Images.Select(url => new AnhGiay
                        {
                            ImageShoeID = Guid.NewGuid(),
                            ShoeDetailID = chiTiet.ShoeDetailID,
                            DuongDanAnh = url
                        }));
                    }
                }
            }

            // Transaction ở ngoài vòng lặp
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                if (chiTietList.Any()) _db.ChiTietGiays.AddRange(chiTietList);
                if (anhGiayList.Any()) _db.AnhGiays.AddRange(anhGiayList);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                return ApiResponse<CreateVM>.SuccessResponse(createVM, "Thêm/Cập nhật chi tiết giày và ảnh thành công");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return ApiResponse<CreateVM>.FailResponse("Exception", $"Có lỗi khi thêm/cập nhật chi tiết giày: {ex.Message}");
            }
        }

        public ApiResponse<string> Delete(Guid id)
        {
            try
            {
                var obj = _db.ChiTietGiays.Find(id);
                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy đang xóa");

                obj.IsDelete = true;
                _db.SaveChanges();
                return ApiResponse<string>.SuccessResponse("Success", "Đã xóa chi tiết giày");
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
                var obj = await _db.ChiTietGiays
                .Include(c => c.Giay)
                .Include(c => c.DanhMuc)
                .Include(c => c.ThuongHieu)
                .Include(c => c.ChatLieu)
                .Include(c => c.MauSac)
                .Include(c => c.KichThuoc)
                .FirstOrDefaultAsync(c => c.ShoeDetailID == id);

                if (obj == null) return ApiResponse<string>.FailResponse("ID_ShoeDetail_Not_Found", "Không tìm thấy giầy muốn khôi phục");

                if (obj.Giay == null || obj.Giay.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Sản phẩm {obj.Giay?.TenGiay ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");
                if (obj.ThuongHieu == null || obj.ThuongHieu.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Tên thương hiệu {obj.ThuongHieu?.TenThuongHieu ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");
                if (obj.ChatLieu == null || obj.ChatLieu.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Chất liệu {obj.ChatLieu?.TenChatLieu ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");
                if (obj.DanhMuc == null || obj.DanhMuc.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Danh mục {obj.DanhMuc?.TenDanhMuc ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");
                if (obj.MauSac == null || obj.MauSac.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Màu sắc {obj.MauSac?.TenMau ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");
                if (obj.KichThuoc == null || obj.KichThuoc.IsDelete == true)
                    return ApiResponse<string>.FailResponse("Product_Delete", $"Kích thước {obj.KichThuoc?.TenKichThuoc ?? "không tên"} đang bị lỗi hoặc đã ngưng hoạt động hãy kiểm tra lại sau.");


                obj.IsDelete = false;
                await _db.SaveChangesAsync();
                return ApiResponse<string>.SuccessResponse("Success", "Đã khôi phục thành công");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.FailResponse("Error", "Lỗi khi xóa:" + ex.Message);
            }
        }
    }
}

