using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GiayController : Controller
    {
        private readonly IGiayService _sanPhamService;
        private readonly ApplicationDbContext _db;
        public GiayController(IGiayService sanPhamService, ApplicationDbContext db)
        {
            _sanPhamService = sanPhamService;
            _db = db;
        }

        public IActionResult Index()
        {
            var list = _sanPhamService.GetAll().ToList();
            var tongSoLuongDict = _db.ChiTietGiays
                .GroupBy(ct => ct.ShoeID)
                .ToDictionary(g => g.Key, g => g.Sum(ct => ct.SoLuong));
            var viewModel = list.Select(g => new GiayWithSoLuongVM
            {
                Giay = g,
                TongSoLuong = tongSoLuongDict.ContainsKey(g.ShoeID) ? tongSoLuongDict[g.ShoeID] : 0
            }).ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Giay model)
        {
            if (ModelState.IsValid)
            {
                _sanPhamService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var sp = _sanPhamService.GetById(id);
            if (sp == null) return NotFound();
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Giay model)
        {
            if (ModelState.IsValid)
            {
                _sanPhamService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            _sanPhamService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var giay = _db.Giays.Find(id);
            if (giay == null) return NotFound();
            var chiTietList = _db.ChiTietGiays
                .Include(ct => ct.DanhMuc)
                .Include(ct => ct.ThuongHieu)
                .Include(ct => ct.ChatLieu)
                .Include(ct => ct.KichThuoc)
                .Include(ct => ct.MauSac)
                .Where(ct => ct.ShoeID == id)
                .ToList();
            var firstDetail = chiTietList.FirstOrDefault();
            var viewModel = new GiayFullInfoVM
            {
                Giay = giay,
                TenDanhMuc = firstDetail?.DanhMuc?.TenDanhMuc ?? "",
                TenThuongHieu = firstDetail?.ThuongHieu?.TenThuongHieu ?? "",
                TenChatLieu = firstDetail?.ChatLieu?.TenChatLieu ?? "",
                ChiTietGiays = chiTietList,
                TongSoLuong = chiTietList.Sum(ct => ct.SoLuong)
            };
            return View(viewModel);
        }
    }
} 