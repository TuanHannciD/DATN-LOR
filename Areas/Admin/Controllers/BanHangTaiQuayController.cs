using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Models;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DATN_Lor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanHangTaiQuayController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IBanHangTaiQuayService _banHangTaiQuayService;
        public BanHangTaiQuayController(ApplicationDbContext db, IBanHangTaiQuayService banHangTaiQuayService)
        {
            _db = db;
            _banHangTaiQuayService = banHangTaiQuayService;
        }
        // GET: Admin/BanHangTaiQuay
        public IActionResult Index()
        {
            var tuVanSanPham = _banHangTaiQuayService.SearchSanPham("");
            ViewBag.TuVanSanPham = tuVanSanPham;

            // Lấy UserID từ session
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            Guid? userId = null;
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                userId = _db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap)?.UserID;
            }
            List<ChiTietGioHang> cartItems = new();
            if (userId != null)
            {
                var cart = _db.GioHangs.FirstOrDefault(g => g.UserID == userId);
                if (cart != null)
                {
                    cartItems = _db.ChiTietGioHangs
                        .Where(c => c.CartID == cart.CartID)
                        .Include(c => c.ChiTietGiay)
                        .ThenInclude(g => g.Giay)
                        .Include(c => c.ChiTietGiay.MauSac)
                        .Include(c => c.ChiTietGiay.KichThuoc)
                        .Include(c => c.ChiTietGiay.ChatLieu)
                        .Include(c => c.ChiTietGiay.ThuongHieu)
                        .Include(c => c.ChiTietGiay.DanhMuc)
                        .ToList();
                }
            }
            ViewBag.CartItems = cartItems;
            return View();
        }

        [HttpGet]
        public JsonResult SearchSanPham(string keyword)
        {
            var result = _banHangTaiQuayService.SearchSanPham(keyword);
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            Guid? userId = null;
            if (!string.IsNullOrEmpty(tenDangNhap))
            {
                userId = _db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap)?.UserID;
            }
            List<ChiTietGioHang> cartItems = new();
            if (userId != null)
            {
                var cart = _db.GioHangs.FirstOrDefault(g => g.UserID == userId);
                if (cart != null)
                {
                    cartItems = _db.ChiTietGioHangs
                        .Where(c => c.CartID == cart.CartID)
                        .Include(c => c.ChiTietGiay)
                        .ThenInclude(g => g.Giay)
                        .Include(c => c.ChiTietGiay.MauSac)
                        .Include(c => c.ChiTietGiay.KichThuoc)
                        .Include(c => c.ChiTietGiay.ChatLieu)
                        .Include(c => c.ChiTietGiay.ThuongHieu)
                        .Include(c => c.ChiTietGiay.DanhMuc)
                        .ToList();
                }
            }
            // Có thể trả về PartialView hoặc Json tuỳ mục đích sử dụng
            return Json(cartItems);
        }

        [HttpPost]
        public IActionResult UpdateCart(Guid shoeDetailId, string actionType)
        {
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            var user = _db.NguoiDungs.FirstOrDefault(u => u.TenDangNhap == tenDangNhap);
            if (user == null) return RedirectToAction("Index");

            var cart = _db.GioHangs.FirstOrDefault(g => g.UserID == user.UserID);
            if (cart == null)
            {
                cart = new GioHang { CartID = Guid.NewGuid(), UserID = user.UserID };
                _db.GioHangs.Add(cart);
                _db.SaveChanges();
            }

            var cartItem = _db.ChiTietGioHangs.FirstOrDefault(c => c.CartID == cart.CartID && c.ShoeDetailID == shoeDetailId);

            if (actionType == "add" || actionType == "increase")
            {
                if (cartItem == null)
                {
                    // Lấy ChiTietGiay từ database để lấy thông tin KichThuoc
                    var chiTietGiay = _db.ChiTietGiays
                        .Include(ctg => ctg.KichThuoc)
                        .FirstOrDefault(ctg => ctg.ShoeDetailID == shoeDetailId);

                    string tenKichThuoc = chiTietGiay?.KichThuoc?.TenKichThuoc ?? "";

                    cartItem = new ChiTietGioHang
                    {
                        CartDetailID = Guid.NewGuid(),
                        CartID = cart.CartID,
                        ShoeDetailID = shoeDetailId,
                        KichThuoc = tenKichThuoc,
                        SoLuong = 1
                    };
                    _db.ChiTietGioHangs.Add(cartItem);
                }
                else
                {
                    cartItem.SoLuong += 1;
                    _db.ChiTietGioHangs.Update(cartItem);
                }
            }
            else if (actionType == "decrease" && cartItem != null)
            {
                cartItem.SoLuong -= 1;
                if (cartItem.SoLuong <= 0)
                    _db.ChiTietGioHangs.Remove(cartItem);
                else
                    _db.ChiTietGioHangs.Update(cartItem);
            }
            else if (actionType == "remove" && cartItem != null)
            {
                _db.ChiTietGioHangs.Remove(cartItem);
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
} 