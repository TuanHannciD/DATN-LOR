using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using AuthDemo.Models.Enums;
using AuthDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly ApplicationDbContext _db;
        public HoaDonService(ApplicationDbContext db)
        {
            _db = db;
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
                TenKhachHang = x.NguoiDung.HoTen,
                SoDienThoai = x.NguoiDung.SoDienThoai,
                Email = x.NguoiDung.Email,
                DiaChi = x.NguoiDung != null && x.NguoiDung.DiaChis != null && x.NguoiDung.DiaChis.Any()
                    ? x.NguoiDung.DiaChis.Select(d => d.DiaChiDayDu).FirstOrDefault()
                    : "",
                HinhThucThanhToan = x.PhuongThucThanhToan,

                HinhThucVanChuyen = x.PhuongThucVanChuyen,
                TrangThaiDisplay = x.TrangThai.GetDisplayName(),
                HinhThucThanhToanDisplay = x.PhuongThucThanhToan.GetDisplayName(),
                HinhThucVanChuyenDisplay = x.PhuongThucVanChuyen.GetDisplayName(),
            }).ToList();
            return hoaDons;
        }
        public GetHoaDonByIdVM GetHoaDonById(int id)
        {
            // Chưa có logic cụ thể, chỉ trả về một đối tượng rỗng
            return new GetHoaDonByIdVM();
        }
        public async Task<Result<HoaDon>> CreateHoaDon(CreateHoaDonVM createHoaDonVM, string tenDangNhap)
        {   
            // Kiểm tra thông tin đăng nhập
            var userCrete = await _db.NguoiDungs.FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);
            if (userCrete == null)
                return Result<HoaDon>.Fail("Không tìm thấy người dùng.");

            var userHoaDon = await _db.NguoiDungs
                .Include(u => u.DiaChis)
                .FirstOrDefaultAsync(u => u.UserID == createHoaDonVM.UserID);

            // Kiểm tra người dùng trong hóa đơn có tồn tại
            if (userHoaDon == null)
                return Result<HoaDon>.Fail("Không tìm thấy người dùng với ID đã cung cấp.");

            var gioHang = await _db.GioHangs.FirstOrDefaultAsync(g => g.UserID == userCrete.UserID);
           if (gioHang == null)
                return Result<HoaDon>.Fail("Không tìm thấy giỏ hàng cho người dùng.");

            //  Kiểm tra enum phương thức thanh toán và vận chuyển
            if (!Enum.TryParse(createHoaDonVM.HinhThucThanhToan, out PhuongThucThanhToan phuongThucThanhToan))
                return Result<HoaDon>.Fail("Phương thức thanh toán không hợp lệ.");
            
            if (!Enum.TryParse(createHoaDonVM.HinhThucVanChuyen, out PhuongThucVanChuyen phuongThucVanChuyen))
                return Result<HoaDon>.Fail("Phương thức vận chuyển không hợp lệ.");

                //Tính tiền cho vào hóa đơn sau tất cả loại giảm
            var tongTienVM = TinhTienHoaDon(gioHang.CartID, createHoaDonVM.GiamGiaPhanTram, createHoaDonVM.GiamGiaTienMat);
            if (tongTienVM.TongThanhToan <= 0)
                return Result<HoaDon>.Fail("Tổng tiền thanh toán không hợp lệ.");



            var hoaDon = new HoaDon
            {
                BillID = Guid.NewGuid(),
                UserID = createHoaDonVM.UserID,
                HoTen = userHoaDon.HoTen ?? "Chưa có tên",
                Email = userHoaDon.Email ?? "Chưa có email",
                SoDienThoai = userHoaDon.SoDienThoai ?? "Chưa có số điện thoại",
                DiaChi = userHoaDon.DiaChis?.FirstOrDefault()?.DiaChiDayDu ?? "Chưa có địa chỉ",
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

                // Cập nhật giỏ hàng

                return Result<HoaDon>.Success(hoaDon);
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return Result<HoaDon>.Fail($"Lỗi khi tạo hóa đơn: {ex.Message} | Chi tiết: {innerMessage}");
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
    }
}