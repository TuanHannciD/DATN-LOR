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
                var all = _db.SanPhamChiTiets.Select(sp => new {
                    sp.ID_Spct,
                    TenSp = sp.TenSp,
                    Gia = sp.Gia,
                    SoLuong = sp.SoLuongBan
                }).Take(20).ToList();
                return Json(all);
            }
            var tuKhoa = keyword.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = _db.SanPhamChiTiets
                .Where(sp => tuKhoa.All(k => sp.TenSp.ToLower().Contains(k)))
                .Select(sp => new {
                    sp.ID_Spct,
                    TenSp = sp.TenSp,
                    Gia = sp.Gia,
                    SoLuong = sp.SoLuongBan
                })
                .Take(20)
                .ToList();
            return Json(result);
        }
    }
} 