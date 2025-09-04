using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using AuthDemo.Models.Enums;
using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AuthDemo.Common;

namespace AuthDemo.Areas.Admin.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly ApplicationDbContext _db;
        public HoaDonService(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<object> GetTrangThaiList()
        {
            var list = Enum.GetValues(typeof(TrangThaiHoaDon))
                .Cast<TrangThaiHoaDon>()
                .Select(e =>
                {
                    var member = e.GetType().GetMember(e.ToString()).First();
                    var displayAttr = member.GetCustomAttribute<DisplayAttribute>(); // .NET 8 cho phép dùng generic
                    return new
                    {
                        value = (int)e,
                        text = displayAttr?.Name ?? e.ToString()
                    };
                }).ToList<object>();
            return list;
        }

        public List<GetAllHoaDonVM> GetAllHoaDon()
        {
            var hoaDons = _db.HoaDons.Select(x => new GetAllHoaDonVM
            {
                HoaDonID = x.BillID,
                UserID = x.UserID,
                TongTien = x.TongTien,
                NguoiTao = x.NguoiTao ?? "",
                NgayTao = x.NgayTao,
                NguoiCapNhat = x.NguoiCapNhat ?? "",
                NgayCapNhat = x.NgayCapNhat,
                TenKhachHang = x.HoTen,
                DaThanhToan = x.DaThanhToan,
                SoDienThoai = x.SoDienThoai,
                Email = x.Email,
                DiaChi = x.DiaChi,
                HinhThucThanhToan = x.PhuongThucThanhToan,
                TrangThaiHoaDon = x.TrangThai,
                HinhThucVanChuyen = x.PhuongThucVanChuyen,
                TrangThaiDisplay = x.TrangThai.GetDisplayName(),
                HinhThucThanhToanDisplay = x.PhuongThucThanhToan.GetDisplayName(),
                HinhThucVanChuyenDisplay = x.PhuongThucVanChuyen.GetDisplayName(),
            }).ToList();
            return hoaDons;
        }

        //Bộ lọc tìm kiếm 
        public async Task<List<GetAllHoaDonVM>> GetHoaDonsAsync(HoaDonFilter filter)
        {
            var hoaDons = _db.HoaDons.AsQueryable();

            // Filter ngày
            if (filter.StartDate.HasValue)
                hoaDons = hoaDons.Where(h => h.NgayTao >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                hoaDons = hoaDons.Where(h => h.NgayTao <= filter.EndDate.Value);

            // Filter trạng thái
            if (!string.IsNullOrEmpty(filter.TrangThai))
                if (Enum.TryParse<TrangThaiHoaDon>(filter.TrangThai, out var trangthaiEnum))
                {
                    hoaDons = hoaDons.Where(h => h.TrangThai == trangthaiEnum);
                }

            // Filter phương thức thanh toán
            if (!string.IsNullOrEmpty(filter.HinhThuc) &&
                Enum.TryParse<PhuongThucThanhToan>(filter.HinhThuc, out var hinhThucEnum))
            {
                hoaDons = hoaDons.Where(h => h.PhuongThucThanhToan == hinhThucEnum);
            }

            // Các filter khác
            if (!string.IsNullOrEmpty(filter.Phone))
                hoaDons = hoaDons.Where(h => h.SoDienThoai != null && h.SoDienThoai.StartsWith(filter.Phone));

            if (!string.IsNullOrEmpty(filter.IdFilter))
                hoaDons = hoaDons.Where(h => h.BillID.ToString().StartsWith(filter.IdFilter));

            if (!string.IsNullOrEmpty(filter.NameCreateFilter))
                hoaDons = hoaDons.Where(h => !string.IsNullOrEmpty(h.NguoiTao) &&
                h.NguoiTao.StartsWith(filter.NameCreateFilter, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(filter.NameFilter))
            {
                var keywords = filter.NameFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                hoaDons = hoaDons.Where(h => h.UserID != Guid.Empty &&
                keywords.All(k => h.HoTen.Contains(k, StringComparison.OrdinalIgnoreCase)));
            }

            if (filter.TrangThaiTT.HasValue)
                hoaDons = hoaDons.Where(h => h.DaThanhToan == filter.TrangThaiTT.Value);

            // Chuyển sang List và map display name
            var list = await hoaDons.ToListAsync(); // Lấy dữ liệu từ DB trước
            var result = list
                .Select(h => new GetAllHoaDonVM
                {
                    HoaDonID = h.BillID,
                    UserID = h.UserID,
                    TongTien = h.TongTien,
                    NguoiTao = h.NguoiTao,
                    NgayTao = h.NgayTao,
                    NguoiCapNhat = h.NguoiCapNhat,
                    DaThanhToan = h.DaThanhToan,
                    NgayCapNhat = h.NgayCapNhat,
                    TenKhachHang = h.HoTen,
                    SoDienThoai = h.SoDienThoai,
                    Email = h.Email,
                    DiaChi = h.DiaChi,
                    HinhThucThanhToan = h.PhuongThucThanhToan,
                    HinhThucVanChuyen = h.PhuongThucVanChuyen,
                    TrangThaiHoaDon = h.TrangThai,
                    TrangThaiDisplay = h.TrangThai.GetDisplayName(),
                    HinhThucThanhToanDisplay = h.PhuongThucThanhToan.GetDisplayName(),
                    HinhThucVanChuyenDisplay = h.PhuongThucVanChuyen.GetDisplayName()
                })
                .OrderByDescending(h => h.NgayTao)
                .ToList();

            return result;
        }
        public HoaDonDTO GetHoaDonById(int id)
        {
            // Chưa có logic cụ thể, chỉ trả về một đối tượng rỗng
            return new HoaDonDTO();
        }
        public async Task<Result<HoaDonDTO>> CreateHoaDon(CreateHoaDonVM createHoaDonVM, string tenDangNhap)
        {
            // Kiểm tra thông tin đăng nhập
            var userCrete = await _db.NguoiDungs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);
            var userCusTomer = await _db.NguoiDungs
                .Include(u => u.DiaChis)
                .FirstOrDefaultAsync(u => u.UserID == createHoaDonVM.UserID);
            if (userCusTomer == null)
                return Result<HoaDonDTO>.Fail("Không tìm thấy khách hàng với ID đã cung cấp.");

            if (userCrete == null)
                return Result<HoaDonDTO>.Fail("Không tìm thấy người dùng.");
            // Kiểm tra ID người dùng
            if (createHoaDonVM.UserID == null || createHoaDonVM.UserID == Guid.Empty)
                return Result<HoaDonDTO>.Fail("ID người dùng không hợp lệ.");

            // Kiểm tra người dùng trong hóa đơn có tồn tại
            if (userCrete == null)
                return Result<HoaDonDTO>.Fail("Không tìm thấy người dùng với ID đã cung cấp.");

            var gioHang = await _db.GioHangs.FirstOrDefaultAsync(g => g.UserID == userCrete.UserID);
            if (gioHang == null)
                return Result<HoaDonDTO>.Fail("Không tìm thấy giỏ hàng cho người dùng hoặc chưa thêm sản phẩm vào giỏ hàng.");

            // Kiểm tra enum phương thức thanh toán và vận chuyển
            if (createHoaDonVM.HinhThucThanhToan == null && createHoaDonVM.HinhThucVanChuyen == null)
                return Result<HoaDonDTO>.Fail("Phương thức thanh toán và vận chuyển không được để trống.");

            if (!Enum.TryParse(createHoaDonVM.HinhThucThanhToan, out PhuongThucThanhToan phuongThucThanhToan))
                return Result<HoaDonDTO>.Fail("Phương thức thanh toán không hợp lệ.");

            if (!Enum.TryParse(createHoaDonVM.HinhThucVanChuyen, out PhuongThucVanChuyen phuongThucVanChuyen))
                return Result<HoaDonDTO>.Fail("Phương thức vận chuyển không hợp lệ.");

            // Lấy tất cả sản phẩm trong giỏ của user
            var deletedProductsInCart = await _db.ChiTietGioHangs
                .Include(c => c.ChiTietGiay)
                .Where(c => c.GioHang.UserID == createHoaDonVM.UserID && c.ChiTietGiay.IsDelete == true)
                .ToListAsync();

            if (deletedProductsInCart.Count > 0)
            {
                _db.ChiTietGioHangs.RemoveRange(deletedProductsInCart);
                await _db.SaveChangesAsync();

                return Result<HoaDonDTO>.Fail("Một số sản phẩm trong giỏ đã bị ngưng bán và đã được xóa khỏi giỏ hàng. Vui lòng kiểm tra lại.");
            }

            // Tính tiền cho vào hóa đơn sau tất cả loại giảm
            var tongTienVM = TinhTienHoaDon(gioHang.CartID, createHoaDonVM.GiamGiaPhanTram, createHoaDonVM.GiamGiaTienMat);
            if (tongTienVM.TongThanhToan <= 0)
                return Result<HoaDonDTO>.Fail("Tổng tiền thanh toán không hợp lệ.");

            var hoaDon = new HoaDon
            {
                BillID = Guid.NewGuid(),
                UserID = userCusTomer.UserID,
                HoTen = createHoaDonVM.HoTen ?? userCusTomer.HoTen ?? "Khách vãng lai",
                Email = createHoaDonVM.Email ?? userCrete.Email ?? "Chưa có email",
                SoDienThoai = createHoaDonVM.SoDienThoai ?? userCusTomer.SoDienThoai ?? "Chưa có số điện thoại",
                DiaChi = createHoaDonVM.DiaChi ?? userCrete.DiaChis?.FirstOrDefault()?.DiaChiDayDu ?? "Chưa có địa chỉ",
                DaThanhToan = false,
                PhuongThucThanhToan = Enum.Parse<PhuongThucThanhToan>(createHoaDonVM.HinhThucThanhToan),
                PhuongThucVanChuyen = Enum.Parse<PhuongThucVanChuyen>(createHoaDonVM.HinhThucVanChuyen),
                TongTien = tongTienVM.TongThanhToan,
                DaHuy = false,
                GhiChu = "Để tạm chưa sửa",
                NguoiTao = tenDangNhap,
                NgayTao = DateTime.Now,
                TrangThai = TrangThaiHoaDon.ChoXacNhan,
                LyDoGiamGia = createHoaDonVM.LyDo,
                GiamGiaPhanTram = createHoaDonVM.GiamGiaPhanTram,
                GiamGiaTienMat = createHoaDonVM.GiamGiaTienMat,
            };

            try
            {
                _db.HoaDons.Add(hoaDon);
                await _db.SaveChangesAsync();

                var hoaDonDTO = new HoaDonDTO
                {
                    BillID = hoaDon.BillID,
                    HoTen = hoaDon.HoTen,
                    SoDienThoai = hoaDon.SoDienThoai,
                    DiaChi = hoaDon.DiaChi,
                    TongTien = hoaDon.TongTien,
                    NgayTao = hoaDon.NgayTao,
                    PhuongThucThanhToan = hoaDon.PhuongThucThanhToan.GetDisplayName(),
                    PhuongThucVanChuyen = hoaDon.PhuongThucVanChuyen.GetDisplayName()
                };

                // Lưu chi tiết hóa đơn
                foreach (var item in gioHang.ChiTietGioHangs)
                {
                    var chiTietHoaDon = new ChiTietHoaDon
                    {
                        BillID = hoaDon.BillID,
                        ShoeDetailID = item.ShoeDetailID,
                        SoLuong = item.SoLuong,
                        DonGia = item.ChiTietGiay.Gia,
                        ChietKhauPhanTram = item.ChietKhauPhanTram,
                        ChietKhauTienMat = item.ChietKhauTienMat,
                        IsTangKem = item.IsTangKem
                    };
                    _db.ChiTietHoaDons.Add(chiTietHoaDon);
                }

                // // Cập nhật số lượng sản phẩm trong kho
                // foreach (var item in gioHang.ChiTietGioHangs)
                // {
                //     var chiTietGiay = await _db.ChiTietGiays.FirstOrDefaultAsync(c => c.ShoeDetailID == item.ShoeDetailID);
                //     if (chiTietGiay != null)
                //     {
                //         chiTietGiay.SoLuong -= item.SoLuong;
                //         if (chiTietGiay.SoLuong < 0)
                //         {
                //             chiTietGiay.SoLuong = 0; // Đảm bảo số lượng tồn không âm
                //         }
                //         _db.ChiTietGiays.Update(chiTietGiay);
                //     }
                // }

                _db.ChiTietGioHangs.RemoveRange(gioHang.ChiTietGioHangs); // Xóa chi tiết giỏ hàng
                await _db.SaveChangesAsync();

                return Result<HoaDonDTO>.Success(hoaDonDTO);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return Result<HoaDonDTO>.Fail($"Lỗi khi tạo hóa đơn: {ex.Message} | Chi tiết: {innerMessage}");
            }
        }

        public HoaDonTongTienVM TinhTienHoaDon(Guid cartID, decimal? giamGiaPhanTram, decimal? giamGiaTienMat)
        {
            var gioHang = _db.GioHangs
                .Include(g => g.ChiTietGioHangs)
                .ThenInclude(c => c.ChiTietGiay)
                .FirstOrDefault(g => g.CartID == cartID);

            if (gioHang == null || gioHang.ChiTietGioHangs == null || !gioHang.ChiTietGioHangs.Any())
            {
                return new HoaDonTongTienVM
                {
                    TongTienHang = 0,
                    GiamGiaHoaDon = 0,
                    TongThanhToan = 0
                };
            }

            decimal tongTienHang = 0;
            decimal giamGiaHoaDon = 0;
            decimal giamGiaSanPham = 0;

            foreach (var item in gioHang.ChiTietGioHangs)
            {
                var giaGoc = item.ChiTietGiay.Gia;
                var soLuong = item.SoLuong;
                // Tính giá gốc để so sánh
                tongTienHang += giaGoc * soLuong;
                // Tính chiết khấu % nếu có nếu có
                if (item.ChietKhauPhanTram.HasValue && item.ChietKhauPhanTram.Value > 0)
                {
                    giamGiaSanPham += giaGoc * soLuong * (item.ChietKhauPhanTram.Value / 100);
                }
                // Tính chiết khấu tiền mặt nếu có
                if (item.ChietKhauTienMat.HasValue && item.ChietKhauTienMat.Value > 0)
                {
                    giamGiaSanPham += item.ChietKhauTienMat.Value * soLuong;
                }
            }
            // Trừ chiết khấu sản phẩm vào tổng tiền hàng
            tongTienHang -= giamGiaSanPham;

            if (giamGiaPhanTram.HasValue && giamGiaPhanTram.Value > 0)
            {
                giamGiaHoaDon = tongTienHang * (giamGiaPhanTram.Value / 100);
            }

            if (giamGiaTienMat.HasValue && giamGiaTienMat.Value > 0)
            {
                giamGiaHoaDon += giamGiaTienMat.Value;
            }

            decimal tongThanhToan = tongTienHang - giamGiaHoaDon;

            return new HoaDonTongTienVM
            {
                TongTienHang = tongTienHang,
                GiamGiaHoaDon = giamGiaHoaDon,
                TongThanhToan = tongThanhToan
            };
        }
        public async Task<Result<string>> XacnhanTienMat(bool confirmdone, Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return Result<string>.Fail("ID đơn hàng không hợp lệ.");
            }
            if (!confirmdone)
            {
                return Result<string>.Fail("Bạn chưa xác nhận đã thanh toán.");
            }
            var hoaDon = await _db.HoaDons.FindAsync(orderId);
            if (hoaDon == null)
            {
                return Result<string>.Fail("Không tìm thấy hóa đơn với ID đã cung cấp.");
            }
            if (hoaDon.TrangThai == TrangThaiHoaDon.DaHuy)
            {
                return Result<string>.Fail("Hóa đơn đã bị hủy, không thể cập nhật trạng thái thanh toán.");
            }
            if (hoaDon.DaThanhToan == true)
            {
                return Result<string>.Fail("Hóa đơn đã được thanh toán trước đó.");
            }
            hoaDon.DaThanhToan = true;
            hoaDon.TrangThai = TrangThaiHoaDon.DaGiao;
            await _db.SaveChangesAsync();

            return Result<string>.Success("Cập nhật trạng thái thanh toán thành công.");
        }
        // Cập nhật trạng thái hóa đơn tại hóa đơn
        public async Task<ApiResponse<UpdateTrangThaiResponse>> UpdateTrangThai(Guid HoaDonID)
        {
            var hoaDon = _db.HoaDons.Find(HoaDonID);

            if (hoaDon == null)
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Null", "Không tìm thấy hóa đơn");
            }

            var currentStatus = hoaDon.TrangThai;
            int nextStatus = (int)currentStatus + 1;

            if (currentStatus == TrangThaiHoaDon.DaGiao || currentStatus == TrangThaiHoaDon.DaHuy)
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Error", "Hóa đơn đã được giao hoặc ở trạng thái cuối.");
            }
            if (currentStatus == TrangThaiHoaDon.DangGiaoHang)
            {
                hoaDon.DaThanhToan = true;
                _db.SaveChanges();
            }
            var errors = new List<string>();
            var hdct = _db.ChiTietHoaDons.Where(c => c.BillID == HoaDonID).ToList();
            if (hoaDon.TrangThai == TrangThaiHoaDon.ChoXacNhan && hoaDon.PhuongThucThanhToan == PhuongThucThanhToan.TienMat)
            {
                foreach (var ct in hdct)
                {
                    // Lấy chi tiết giày trong kho theo ShoeDetailID
                    var giay = _db.ChiTietGiays
                        .Include(ct => ct.Giay)
                        .Include(ct => ct.MauSac)
                        .Include(ct => ct.KichThuoc)
                        .FirstOrDefault(g => g.ShoeDetailID == ct.ShoeDetailID);
                    if (giay == null)
                    {
                        errors.Add($"Không tìm thấy chi tiết giày với ID: {ct.ShoeDetailID}");
                        continue; // Bỏ qua nếu không tìmTạo ra mực định phải  thấy giày
                    }
                    else
                    {
                        var tenGiay = giay.Giay?.TenGiay ?? "(Chưa có tên)";
                        var mau = giay.MauSac?.TenMau ?? "(Chưa có màu)";
                        var kichThuoc = giay.KichThuoc?.TenKichThuoc ?? "(Chưa có size)";
                        if (giay.SoLuong == 0)
                        {
                            errors.Add($"Sản phẩm đã hết hàng: {tenGiay} ({mau}, {kichThuoc})");
                        }
                        if (ct.SoLuong > giay.SoLuong)
                        {
                            errors.Add($"Số lượng trong kho không đủ cho sản phẩm: {tenGiay} ({mau}, {kichThuoc})");
                        }
                        if (giay.IsDelete == true)
                        {
                            errors.Add($"Sản phẩm đã ngưng bán: {tenGiay} ({mau}, {kichThuoc})");
                        }

                        else
                        {
                            // Trừ số lượng tồn kho
                            giay.SoLuong -= ct.SoLuong;

                            if (giay.SoLuong < 0)
                            {
                                giay.SoLuong = 0; // tránh âm
                            }
                        }
                    }
                }
            }
            if (errors.Any())
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Error", string.Join("<br/>", errors));
            }
            _db.SaveChanges();

            hoaDon.TrangThai = (TrangThaiHoaDon)nextStatus;
            await _db.SaveChangesAsync();

            var responseData = new UpdateTrangThaiResponse
            {
                Id = hoaDon.BillID,
                NewStatus = hoaDon.TrangThai,
                NewStatusDisplay = hoaDon.TrangThai.GetDisplayName()
            };

            return ApiResponse<UpdateTrangThaiResponse>.SuccessResponse(responseData, "Cập nhật trạng thái thành công");
        }

        // Cập nhật trạng thái hóa đơn tại quầy 
        public async Task<ApiResponse<UpdateTrangThaiResponse>> UpdateTrangThaiPOS(Guid HoaDonID)
        {
            var hoaDon = _db.HoaDons.Find(HoaDonID);

            if (hoaDon == null)
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Null", "Không tìm thấy hóa đơn");
            }

            var currentStatus = hoaDon.TrangThai;
            int nextStatus = (int)currentStatus + 1;

            if (currentStatus == TrangThaiHoaDon.DaGiao || currentStatus == TrangThaiHoaDon.DaHuy)
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Error", "Hóa đơn đã được giao hoặc ở trạng thái cuối.");
            }
            if (currentStatus == TrangThaiHoaDon.DangGiaoHang)
            {
                hoaDon.DaThanhToan = true;
                _db.SaveChanges();
            }
            var errors = new List<string>();
            var hdct = _db.ChiTietHoaDons.Where(c => c.BillID == HoaDonID).ToList();
            if (hoaDon.TrangThai == TrangThaiHoaDon.ChoXacNhan && hoaDon.PhuongThucThanhToan == PhuongThucThanhToan.TienMat)
            {
                foreach (var ct in hdct)
                {
                    // Lấy chi tiết giày trong kho theo ShoeDetailID
                    var giay = _db.ChiTietGiays
                        .Include(ct => ct.Giay)
                        .Include(ct => ct.MauSac)
                        .Include(ct => ct.KichThuoc)
                        .FirstOrDefault(g => g.ShoeDetailID == ct.ShoeDetailID);
                    if (giay == null)
                    {
                        errors.Add($"Không tìm thấy chi tiết giày với ID: {ct.ShoeDetailID}");
                        continue; // Bỏ qua nếu không tìmTạo ra mực định phải  thấy giày
                    }
                    else
                    {
                        var tenGiay = giay.Giay?.TenGiay ?? "(Chưa có tên)";
                        var mau = giay.MauSac?.TenMau ?? "(Chưa có màu)";
                        var kichThuoc = giay.KichThuoc?.TenKichThuoc ?? "(Chưa có size)";
                        if (giay.SoLuong == 0)
                        {
                            errors.Add($"Sản phẩm đã hết hàng: {tenGiay} ({mau}, {kichThuoc})");
                        }
                        if (ct.SoLuong > giay.SoLuong)
                        {
                            errors.Add($"Số lượng trong kho không đủ cho sản phẩm: {tenGiay} ({mau}, {kichThuoc})");
                        }
                        if (giay.IsDelete == true)
                        {
                            errors.Add($"Sản phẩm đã ngưng bán: {tenGiay} ({mau}, {kichThuoc})");
                        }
                    }
                }
            }
            if (errors.Any())
            {
                return ApiResponse<UpdateTrangThaiResponse>.FailResponse("Error", string.Join("<br/>", errors));
            }
            _db.SaveChanges();

            hoaDon.TrangThai = (TrangThaiHoaDon)nextStatus;
            await _db.SaveChangesAsync();

            var responseData = new UpdateTrangThaiResponse
            {
                Id = hoaDon.BillID,
                NewStatus = hoaDon.TrangThai,
                NewStatusDisplay = hoaDon.TrangThai.GetDisplayName()
            };

            return ApiResponse<UpdateTrangThaiResponse>.SuccessResponse(responseData, "Cập nhật trạng thái thành công");
        }


        public async Task<ApiResponse<object>> HuyHoaDon(Guid id)
        {
            var hoadon = await _db.HoaDons.FindAsync(id);
            if (hoadon == null)
                return ApiResponse<object>.FailResponse("ID_HoaDon_Not_Found", "Không tìm thấy hóa đơn");

            string message;


            hoadon.TrangThai = TrangThaiHoaDon.DaHuy;
            message = "Đã hủy hóa đơn";


            await _db.SaveChangesAsync();
            var trangThaiDisplay = hoadon.TrangThai.GetDisplayName();

            // Trả về object chứa trạng thái enum chuẩn để JS map màu
            return ApiResponse<object>.SuccessResponse(new { newStatus = trangThaiDisplay }, message);
        }


        public class UpdateTrangThaiResponse
        {
            public Guid Id { get; set; }
            public TrangThaiHoaDon NewStatus { get; set; }

            public string NewStatusDisplay { get; set; }
        }




    }


}
