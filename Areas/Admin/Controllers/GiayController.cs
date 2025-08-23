using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthDemo.Helpers;
using AuthDemo.Models.Enums;
using System.Threading.Tasks;
using static AuthDemo.Models.ViewModels.GiayVM;

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
            .Where(ct => !ct.IsDelete) // chỉ tính các chi tiết chưa xóa
            .GroupBy(ct => ct.ShoeID)
            .ToDictionary(g => g.Key, g => g.Sum(ct => ct.SoLuong));

            var giayEntities = _db.Giays.AsNoTracking().ToList();


            var viewModel = list.Select(g =>
            {
                var giayDb = giayEntities.FirstOrDefault(x => x.ShoeID == g.ShoeID);
                var trangThai = giayDb?.TrangThai.GetDisplayName() ?? "Chưa xác định";
                // Lấy TongSoLuong bằng TryGetValue
                if (!tongSoLuongDict.TryGetValue(g.ShoeID, out var tongSoLuong))
                {
                    tongSoLuong = 0;
                }

                return new GiayWithSoLuongVM
                {
                    Giay = g,
                    TrangThai = trangThai,
                    TongSoLuong = tongSoLuong,
                    NguoiCapNhat = giayDb?.NguoiCapNhat,
                    NgayCapNhat = giayDb?.NgayCapNhat,
                    NguoiTao = giayDb?.NguoiTao,
                    NgayTao = giayDb?.NgayTao
                };
            }).ToList();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetAllDelete()
        {
            try
            {
                var list = _sanPhamService.GetAllDelete().ToList();

                var tongSoLuongDict = _db.ChiTietGiays
                    .Where(ct => !ct.IsDelete) // chỉ tính các chi tiết chưa xóa
                    .GroupBy(ct => ct.ShoeID)
                    .ToDictionary(g => g.Key, g => g.Sum(ct => ct.SoLuong));

                var giayEntities = _db.Giays.AsNoTracking().ToList();

                var viewModel = list.Select(g =>
                {
                    var giayDb = giayEntities.FirstOrDefault(x => x.ShoeID == g.ShoeID);
                    var trangThai = giayDb?.TrangThai.GetDisplayName() ?? "Chưa xác định";

                    tongSoLuongDict.TryGetValue(g.ShoeID, out var tongSoLuong);

                    return new GiayWithSoLuongVM
                    {
                        Giay = g,
                        TrangThai = trangThai,
                        TongSoLuong = tongSoLuong,
                        NguoiCapNhat = giayDb?.NguoiCapNhat,
                        NgayCapNhat = giayDb?.NgayCapNhat,
                        NguoiTao = giayDb?.NguoiTao,
                        NgayTao = giayDb?.NgayTao
                    };
                }).ToList();

                //Trả về JSON cho AJAX
                return Json(new { success = true, data = viewModel });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.TrangThai = EnumHelper.GetEnumSelectList<TrangThai>();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GiayCreate model)
        {
            var response = await _sanPhamService.AddAsync(model);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var giay = _sanPhamService.GetById(id);
            if (giay == null) return NotFound();
            ViewBag.TrangThai = EnumHelper.GetEnumSelectList<TrangThai>();
            var vm = new GiayVM
            {
                ShoeID = giay.ShoeID,
                TenGiay = giay.TenGiay,
                MaGiayCode = giay.MaGiayCode,
                MoTa = giay.MoTa,
                TrangThai = giay.TrangThai.GetDisplayName(),
                NguoiCapNhat = giay.NguoiCapNhat,
                NgayCapNhat = giay.NgayCapNhat

            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GiayVM vm)
        {
            var giay = _sanPhamService.GetById(vm.ShoeID);
            if (giay == null) return NotFound();

            giay.TenGiay = vm.TenGiay;
            giay.MaGiayCode = vm.MaGiayCode;
            giay.MoTa = vm.MoTa;
            giay.TrangThai = Enum.Parse<TrangThai>(vm.TrangThai);
            giay.NguoiCapNhat = User.Identity?.Name ?? "ad";
            giay.NgayCapNhat = DateTime.Now;

            if (ModelState.IsValid)
            {
                _sanPhamService.Update(giay);
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _sanPhamService.Delete(id);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Restore(Guid id)
        {
            if (id == Guid.Empty)
                return Json(new { success = false, message = "Lỗi ID trống hoặc lỗi khác" });
            var response = await _sanPhamService.Restore(id);
            return Json(new
            {
                success = response.Success,
                message = response.Message
            });
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
                TrangThai = giay.TrangThai.GetDisplayName(),
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
