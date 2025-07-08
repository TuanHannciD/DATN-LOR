using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Models;

namespace DATN_Lor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BanHangTaiQuayController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BanHangTaiQuayController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Admin/BanHangTaiQuay
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult SearchSanPham(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                var all = _db.ChiTietGiays.Select(sp => new {
                    sp.ShoeDetailID,
                    TenSp = sp.Giay.TenGiay,
                    sp.Gia,
                    sp.SoLuong,
                    MauSac = sp.MauSac.TenMau,
                    KichThuoc = sp.KichThuoc.TenKichThuoc,
                    ChatLieu = sp.ChatLieu.TenChatLieu,
                    ThuongHieu = sp.ThuongHieu.TenThuongHieu,
                    DanhMuc = sp.DanhMuc.TenDanhMuc,
                    Giay = sp.Giay.TenGiay
                }).Take(20).ToList();
                return Json(all);
            }
            var tuKhoa = keyword.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = _db.ChiTietGiays
                .Where(sp => tuKhoa.All(k => sp.Giay.TenGiay.ToLower().Contains(k)))
                .Select(sp => new {
                    sp.ShoeDetailID,
                    TenSp = sp.Giay.TenGiay,
                    sp.Gia,
                    sp.SoLuong,
                    MauSac = sp.MauSac.TenMau,
                    KichThuoc = sp.KichThuoc.TenKichThuoc,
                    ChatLieu = sp.ChatLieu.TenChatLieu,
                    ThuongHieu = sp.ThuongHieu.TenThuongHieu,
                    DanhMuc = sp.DanhMuc.TenDanhMuc,
                    Giay = sp.Giay.TenGiay
                })
                .Take(20)
                .ToList();
            return Json(result);
        }
    }
} 