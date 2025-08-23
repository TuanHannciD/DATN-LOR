using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using AuthDemo.Models.Enums;
using AuthDemo.Helpers;
using AuthDemo.Areas.Admin.Services;
using Newtonsoft.Json.Linq;
using System.Drawing.Drawing2D;


namespace Controllers
{
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GhnService _ghn;
        public HoaDonController(ApplicationDbContext context, GhnService ghn)
        {
            _ghn = ghn;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var provincesJson = await _ghn.GetProvinces();
            var provinces = JObject.Parse(provincesJson)["data"];
            ViewBag.Provinces = provinces;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetDistricts(int provinceId)
        {
            if (provinceId <= 0) return Json(new object[0]);

            var districtsJson = await _ghn.GetDistricts(provinceId);
            var districts = JObject.Parse(districtsJson)["data"];

            // Trả về JS đúng dạng { id, name }
            var result = districts.Select(d => new
            {
                id = (int)d["DistrictID"],
                name = (string)d["DistrictName"]
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> GetWards(int districtId)
        {
            if (districtId <= 0) return Json(new object[0]);

            var wardsJson = await _ghn.GetWards(districtId);
            var wards = JObject.Parse(wardsJson)["data"];

            var result = wards.Select(w => new
            {
                id = (string)w["WardCode"],
                name = (string)w["WardName"]
            }).ToList();

            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> TinhPhi(int districtId, string wardCode)
        {

            if (districtId <= 0 || string.IsNullOrEmpty(wardCode))
                return Json(new { success = false, ship = 0 });

            try
            {
                var feeJson = await _ghn.TinhPhi(districtId, wardCode);
                var obj = JObject.Parse(feeJson);

                // Nếu GHN trả lỗi, data sẽ null
                var total = obj["data"]?["total"]?.Value<int>() ?? 0;

                return Json(new { success = total > 0, ship = total });
            }
            catch (Exception ex)
            {
                // log lỗi để debug
                Console.WriteLine(ex.Message);
                return Json(new { success = false, ship = 0, error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult PlaceOrder(string name, string phone, string address, int provinceId, int districtId, string wardCode, int shippingFee)
        {
            // TODO: lưu vào bảng HoaDon
            // HoaDon.DiaChi = address + " - " + wardCode + " - " + districtId + " - " + provinceId
            // HoaDon.PhiShip = shippingFee
            // HoaDon.TongTien = TienHang + shippingFee
            return RedirectToAction("Success");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(List<Guid> selectedIds, List<int> quantities)
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
                var trangthai = item.ChiTietGiay.IsDelete;
                if (trangthai == true)
                {
                    warnings.Add($"Sản phẩm {item.ChiTietGiay.Giay.TenGiay} ({item.ChiTietGiay.MauSac.TenMau}, {item.ChiTietGiay.KichThuoc.TenKichThuoc}) đã ngừng kinh doanh và sẽ không thể đặt hàng.");
                    hasInvalid = true;

                }
                if (stock == 0)
                {
                    warnings.Add($"Sản phẩm '{item.ChiTietGiay.Giay.TenGiay} ({item.ChiTietGiay.MauSac.TenMau}, {item.ChiTietGiay.KichThuoc.TenKichThuoc})' đã hết hàng và sẽ không thể đặt hàng.");

                    hasInvalid = true;
                }
                else if (requestedQty > stock)
                {
                    warnings.Add($"Sản phẩm '{item.ChiTietGiay.Giay.TenGiay} ({item.ChiTietGiay.MauSac.TenMau}, {item.ChiTietGiay.KichThuoc.TenKichThuoc})' chỉ còn {stock} sản phẩm. Số lượng đã được cập nhật.");
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
            //  Gọi GHN API để lấy danh sách tỉnh/quận/xã
            var provincesJson = await _ghn.GetProvinces();
            var provinces = JObject.Parse(provincesJson)["data"];
            ViewBag.Provinces = provinces;

            //decimal ship = 0;
            //var phiResult = await _ghn.TinhPhi(districtId, wardCode);
            // Nếu mọi thứ hợp lệ → sang trang thanh toán


            ViewBag.TongTien1 = cartItems.Sum(x => x.SoLuong * x.ChiTietGiay.Gia);
            ViewBag.TongTien = (cartItems.Sum(x => x.SoLuong * x.ChiTietGiay.Gia));
            return View(cartItems); // view: Views/HoaDon/CheckOut.cshtml
        }

        [HttpPost]
        public IActionResult DatHang(string hoten, string email, string sdt, string diachi_full, string ghichu, string phuongthuc, List<Guid> selectedIds,
    List<int> quantities, decimal ship)
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

                if (product == null || product.ChiTietGiay.SoLuong == 0 || product.ChiTietGiay.IsDelete == true)
                {
                    warnings.Add($"Sản phẩm '{product?.ChiTietGiay?.Giay?.TenGiay}' đã hết hàng và không thể đặt hoặc đã ngừng kinh doanh.");

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
                tongTien += (cartItems[i].ChiTietGiay.Gia * quantities[i]) + ship;
            }

            var hoaDon = new HoaDon
            {
                BillID = Guid.NewGuid(),
                UserID = userId,
                HoTen = hoten,
                Email = email,
                SoDienThoai = sdt,
                DiaChi = diachi_full,
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

            // Lấy id user
            var user = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check);
            if (user == null) return RedirectToAction("Login", "Account");
            var userId = user.UserID;

            // Load tất cả hóa đơn của user cùng chi tiết
            var hoaDonsEntities = _context.HoaDons
                .Where(h => h.UserID == userId)
                .OrderByDescending(h => h.NgayTao)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.Giay)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.MauSac)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.KichThuoc)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.AnhGiays)
                .ToList(); // load toàn bộ entity

            // Map sang ViewModel
            var hoaDons = hoaDonsEntities.Select(h => new HoaDonKHVM
            {
                BillID = h.BillID,
                HoTen = h.HoTen,
                SoDienThoai = h.SoDienThoai,
                DiaChi = h.DiaChi,
                TrangThai = (TrangThaiHoaDon)h.TrangThai,
                NgayTao = h.NgayTao,
                TongTien = h.TongTien,
                PhuongThuc = (PhuongThucThanhToan)h.PhuongThucThanhToan,
                ChiTiet = h.ChiTietHoaDons.Select(c => new ChiTietHoaDonKHVM
                {
                    TenSanPham = c.ChiTietGiay.Giay.TenGiay,
                    HinhAnh = c.ChiTietGiay.AnhGiays.FirstOrDefault()?.DuongDanAnh ?? "/images/default.png",
                    Size = c.ChiTietGiay.KichThuoc.TenKichThuoc,
                    MauSac = c.ChiTietGiay.MauSac.TenMau,
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
            var hoaDons = hoaDonsRaw.Select(h =>
            {
                var chiTietList = h.ChiTiet.Select(c => new ChiTietHoaDonKHVM
                {
                    TenSanPham = c.TenSanPham,
                    HinhAnh = c.HinhAnh,
                    Size = c.Size,
                    MauSac = c.MauSac,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia,
                    ThanhTien = c.DonGia * c.SoLuong
                }).ToList();

                // Tính tổng tiền sản phẩm
                decimal tongSanPham = chiTietList.Sum(c => c.ThanhTien);

                // Tính phí ship (TongTien - Tổng tiền sản phẩm)
                decimal phiShip = h.TongTien - tongSanPham;

                ViewBag.ThanhTien = tongSanPham;
                ViewBag.Ship = phiShip;

                return new HoaDonKHVM
                {
                    BillID = h.BillID,
                    HoTen = h.HoTen,
                    SoDienThoai = h.SoDienThoai,
                    DiaChi = h.DiaChi,
                    TrangThai = (TrangThaiHoaDon)h.TrangThai,
                    NgayTao = h.NgayTao,
                    TongTien = h.TongTien,
                    PhuongThuc = (PhuongThucThanhToan)h.PhuongThucThanhToan,
                    ChiTiet = chiTietList
                };
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
                    TrangThai = (TrangThaiHoaDon)h.TrangThai,

                    NgayTao = h.NgayTao,
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
