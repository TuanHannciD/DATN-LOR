using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using AuthDemo.Models.Enums;


namespace Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HoaDonController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // public IActionResult DanhSach()
        // {
        //     var lstDH= db.Don_Hang_Thanh_Toans.ToList();
        //     return View(lstDH); 
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(List<Guid> selectedIds, List<int> quantities)
        {
            var username = HttpContext.Session.GetString("TenDangNhap");
            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Account");

            var userId = _context.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == username)?.UserID;
            var cartId = _context.GioHangs.FirstOrDefault(c => c.UserID == userId)?.CartID;

            var cartItems = _context.ChiTietGioHangs
                .Include(c => c.ChiTietGiay)
                    .ThenInclude(ct => ct.Giay)
                .Include(c => c.ChiTietGiay)
                    .ThenInclude(ct => ct.MauSac)
                .Include(c => c.ChiTietGiay)
                    .ThenInclude(ct => ct.KichThuoc)
                .Where(c => c.CartID == cartId && selectedIds.Contains(c.ShoeDetailID))
                .ToList();

            var warnings = new List<string>();
            bool hasInvalid = false;

            for (int i = 0; i < selectedIds.Count; i++)
            {
                var item = cartItems.FirstOrDefault(x => x.ShoeDetailID == selectedIds[i]);
                if (item == null || item.ChiTietGiay == null) continue;

                int stock = item.ChiTietGiay.SoLuong;
                int requestedQty = quantities[i];

                if (stock == 0)
                {
                    warnings.Add($"Sản phẩm '{item.ChiTietGiay.Giay.TenGiay}' đã hết hàng và sẽ không thể đặt hàng.");
                    item.SoLuong = 0;
                    hasInvalid = true;
                }
                else if (requestedQty > stock)
                {
                    warnings.Add($"Sản phẩm '{item.ChiTietGiay.Giay.TenGiay}' chỉ còn {stock} sản phẩm. Số lượng đã được cập nhật.");
                    item.SoLuong = stock;
                    hasInvalid = true;
                }
            }

            if (hasInvalid)
            {
                _context.SaveChanges();
                TempData["CartWarnings"] = string.Join("<br/>", warnings);
                return RedirectToAction("Index", "Cart");
            }

            // Nếu mọi thứ hợp lệ → sang trang thanh toán
            ViewBag.TongTien = cartItems.Sum(x => x.SoLuong * x.ChiTietGiay.Gia);
            return View(cartItems); // view: Views/HoaDon/CheckOut.cshtml
        }

        [HttpPost]
        public IActionResult DatHang(string hoten, string email, string sdt, string diachi, string ghichu, string phuongthuc, List<Guid> selectedIds,
    List<int> quantities)
        {
            var check = HttpContext.Session.GetString("TenDangNhap");
            if (string.IsNullOrEmpty(check))
                return RedirectToAction("Login", "Account");


            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;
            var cartId = _context.GioHangs.FirstOrDefault(a => a.UserID == userId).CartID;

            //var selectedIds = TempData["selectedIds"]?.ToString()?.Split(',').Select(Guid.Parse).ToList();
            //var quantities = TempData["quantities"]?.ToString()?.Split(',').Select(int.Parse).ToList();

            if (selectedIds == null || quantities == null || selectedIds.Count != quantities.Count)
                return RedirectToAction("Index", "Cart");

            var cartItems = _context.ChiTietGioHangs
                .Include(c => c.ChiTietGiay)
                .ThenInclude(ct => ct.Giay)
                .Where(c => c.CartID == cartId && selectedIds.Contains(c.ShoeDetailID))
                .ToList();

            List<string> warnings = new();
            bool hasInvalid = false;

            for (int i = 0; i < selectedIds.Count; i++)
            {
                var id = selectedIds[i];
                var requestedQty = quantities[i];
                var product = cartItems.FirstOrDefault(c => c.ShoeDetailID == id);

                if (product == null || product.ChiTietGiay.SoLuong == 0)
                {
                    warnings.Add($"Sản phẩm '{product?.ChiTietGiay?.Giay?.TenGiay}' đã hết hàng và không thể đặt.");
                    hasInvalid = true;
                    continue;
                }

                if (requestedQty > product.ChiTietGiay.SoLuong)
                {
                    warnings.Add($"Sản phẩm '{product.ChiTietGiay.Giay.TenGiay}' chỉ còn {product.ChiTietGiay.SoLuong} sản phẩm. Đã cập nhật lại số lượng.");
                    product.SoLuong = product.ChiTietGiay.SoLuong;
                    _context.SaveChanges(); // cập nhật lại số lượng trong giỏ
                    hasInvalid = true;
                }
                if (requestedQty <= product.ChiTietGiay.SoLuong)
                {

                }
            }

            if (hasInvalid)
            {
                TempData["CartWarnings"] = string.Join(" | ", warnings);
                return RedirectToAction("Index", "Cart");
            }

            decimal tongTien = 0;
            for (int i = 0; i < selectedIds.Count; i++)
            {
                tongTien += cartItems[i].ChiTietGiay.Gia * quantities[i];
            }

            var hoaDon = new HoaDon
            {
                BillID = Guid.NewGuid(),
                UserID = userId,
                HoTen = hoten,
                Email = email,
                SoDienThoai = sdt,
                DiaChi = diachi,
                TongTien = tongTien,
                TrangThai = TrangThaiHoaDon.ChoXacNhan,
                DaThanhToan = false,
                PhuongThucThanhToan = Enum.Parse<PhuongThucThanhToan>(phuongthuc),
                DaHuy = false,
                GhiChu = ghichu,
                NgayTao = DateTime.Now,
                NguoiTao = "system",
                NguoiCapNhat = "system",
                NgayCapNhat = DateTime.Now,
            };
            _context.HoaDons.Add(hoaDon);
            _context.SaveChanges();

            for (int i = 0; i < selectedIds.Count; i++)
            {
                var id = selectedIds[i];
                var soLuong = quantities[i];
                var product = cartItems.First(c => c.ShoeDetailID == id);

                var hdct = new ChiTietHoaDon
                {
                    BillDetailID = Guid.NewGuid(),
                    BillID = hoaDon.BillID,
                    ShoeDetailID = id,
                    SoLuong = soLuong,
                    DonGia = product.ChiTietGiay.Gia,
                    NgayTao = DateTime.Now,
                    NguoiTao = "system",
                    NguoiCapNhat = "system",
                    NgayCapNhat = DateTime.Now,
                };
                _context.ChiTietHoaDons.Add(hdct);

                product.ChiTietGiay.SoLuong -= soLuong;
            }

            _context.ChiTietGioHangs.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("DonHang", "HoaDon");
        }


        [HttpGet]
        public IActionResult DonHang()
        {
            var check = HttpContext.Session.GetString("TenDangNhap");

            if (string.IsNullOrEmpty(check))
            {

                return RedirectToAction("Login", "Account");
            }

            //lấy iduser
            var users = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;



            var hoaDonsRaw = _context.HoaDons
    .Where(h => h.UserID == users)
    .OrderByDescending(h => h.NgayTao)
    .Select(h => new
    {
        h.BillID,
        h.HoTen,
        h.SoDienThoai,
        h.DiaChi,
        h.TrangThai,
        h.NgayTao,
        h.TongTien,
        h.PhuongThucThanhToan,
        ChiTiet = _context.ChiTietHoaDons
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.Giay)
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.MauSac)
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.KichThuoc)
            .Where(c => c.BillID == h.BillID)
            .Select(c => new
            {
                TenSanPham = c.ChiTietGiay.Giay.TenGiay,
                HinhAnh = c.ChiTietGiay.Giay.AnhDaiDien,
                Size = c.ChiTietGiay.KichThuoc.TenKichThuoc,
                MauSac = c.ChiTietGiay.MauSac.TenMau,
                c.SoLuong,
                c.DonGia
            }).ToList()
    })
    .ToList();

            // Map sang ViewModel và xử lý Enum
            var hoaDons = hoaDonsRaw.Select(h => new HoaDonKHVM
            {
                BillID = h.BillID,
                HoTen = h.HoTen,
                SoDienThoai = h.SoDienThoai,
                DiaChi = h.DiaChi,
                TrangThai = Enum.GetName(typeof(TrangThaiHoaDon), h.TrangThai) ?? "Chưa xác định",
                NgayTao = h.NgayTao,
                TongTien = h.TongTien,
                PhuongThuc = Enum.GetName(typeof(PhuongThucThanhToan), h.PhuongThucThanhToan) ?? "Chưa xác định",
                ChiTiet = h.ChiTiet.Select(c => new ChiTietHoaDonKHVM
                {
                    TenSanPham = c.TenSanPham,
                    HinhAnh = c.HinhAnh,
                    Size = c.Size,
                    MauSac = c.MauSac,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia
                }).ToList()
            }).ToList();


            return View(hoaDons);


        }
        [HttpGet]
        public IActionResult ChiTietDonHang(Guid HoaDonId)
        {
            var check = HttpContext.Session.GetString("TenDangNhap");

            if (string.IsNullOrEmpty(check))
            {

                return RedirectToAction("Login", "Account");
            }

            //lấy iduser
            var users = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;



            var hoaDonsRaw = _context.HoaDons
    .Where(h => h.BillID == HoaDonId)
    .OrderByDescending(h => h.NgayTao)
    .Select(h => new
    {
        h.BillID,
        h.HoTen,
        h.SoDienThoai,
        h.DiaChi,
        h.TrangThai,
        h.NgayTao,
        h.TongTien,
        h.PhuongThucThanhToan,
        ChiTiet = _context.ChiTietHoaDons
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.Giay)
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.MauSac)
            .Include(c => c.ChiTietGiay).ThenInclude(ctg => ctg.KichThuoc)
            .Where(c => c.BillID == h.BillID)
            .Select(c => new
            {
                TenSanPham = c.ChiTietGiay.Giay.TenGiay,
                HinhAnh = c.ChiTietGiay.Giay.AnhDaiDien,
                Size = c.ChiTietGiay.KichThuoc.TenKichThuoc,
                MauSac = c.ChiTietGiay.MauSac.TenMau,
                c.SoLuong,
                c.DonGia
            }).ToList()
    })
    .ToList();

            // Map sang ViewModel và xử lý Enum
            var hoaDons = hoaDonsRaw.Select(h => new HoaDonKHVM
            {
                BillID = h.BillID,
                HoTen = h.HoTen,
                SoDienThoai = h.SoDienThoai,
                DiaChi = h.DiaChi,
                TrangThai = Enum.GetName(typeof(TrangThaiHoaDon), h.TrangThai) ?? "Chưa xác định",
                NgayTao = h.NgayTao,
                TongTien = h.TongTien,
                PhuongThuc = Enum.GetName(typeof(PhuongThucThanhToan), h.PhuongThucThanhToan) ?? "Chưa xác định",
                ChiTiet = h.ChiTiet.Select(c => new ChiTietHoaDonKHVM
                {
                    TenSanPham = c.TenSanPham,
                    HinhAnh = c.HinhAnh,
                    Size = c.Size,
                    MauSac = c.MauSac,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia
                }).ToList()
            }).ToList();


            return View(hoaDons);


        }
        [HttpGet]
        public IActionResult LayDonHangTheoTrangThai(string trangThai)
        {
            var check = HttpContext.Session.GetString("TenDangNhap");
            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;

            if (userId == null)
                return BadRequest("Người dùng chưa đăng nhập");

            var query = _context.HoaDons
                .Where(h => h.UserID == userId);

            if (!string.IsNullOrEmpty(trangThai))
            {
                // Nếu trangThai là số
                if (int.TryParse(trangThai, out var statusValue))
                {
                    query = query.Where(h => (int)h.TrangThai == statusValue);
                }
                else
                {
                    query = query.Where(h => h.TrangThai.ToString() == trangThai);
                }
            }
            var enumType = typeof(TrangThaiHoaDon);

            var result = query
                .Select(h => new HoaDonKHVM
                {
                    BillID = h.BillID,
                    HoTen = h.HoTen,
                    SoDienThoai = h.SoDienThoai,
                    DiaChi = h.DiaChi,
                    TrangThai = Enum.GetName(enumType, h.TrangThai) ?? "Chưa xác định",
                    NgayTao = h.NgayTao ,
                    TongTien = h.TongTien,
                    ChiTiet = _context.ChiTietHoaDons
                        .Where(c => c.BillID == h.BillID)
                        .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.Giay)
                        .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.KichThuoc)
                        .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.MauSac)
                        .Select(c => new ChiTietHoaDonKHVM
                        {
                            TenSanPham = c.ChiTietGiay.Giay.TenGiay,
                            SoLuong = c.SoLuong,
                            DonGia = c.DonGia,
                            HinhAnh = c.ChiTietGiay.Giay.AnhDaiDien,
                            Size = c.ChiTietGiay.KichThuoc.TenKichThuoc,
                            MauSac = c.ChiTietGiay.MauSac.TenMau
                        }).ToList()
                }).ToList();

            return PartialView("_OrderListPartial", result);
        }



        [HttpPost]
        public IActionResult TrangThai()
        {
            return View();
        }
    }
}
