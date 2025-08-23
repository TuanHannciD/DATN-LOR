using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThuongHieuController : Controller
    {
        private readonly IThuongHieuService _thuongHieuService;
        public ThuongHieuController(IThuongHieuService thuongHieuService)
        {
            _thuongHieuService = thuongHieuService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _thuongHieuService.GetAll();
            // nếu lấy thất bại thì trả về danh sách rỗng
            if (!response.Success)
            {
                return View(new List<KichThuoc>());
            }

            // View nhận đúng IEnumerable<KichThuoc>
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create() => View();
        [HttpGet]
        public async Task<IActionResult> GetAllDelete()
        {
            try
            {
                var list = await _thuongHieuService.GetAllDelete();
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHangSanXuat model)
        {
            var response = await _thuongHieuService.AddAsync(model);
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var hang = _thuongHieuService.GetById(id);
            if (hang == null) return NotFound();
            return View(hang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ThuongHieu model)
        {
            if (ModelState.IsValid)
            {
                _thuongHieuService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _thuongHieuService.Delete(id);
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
            var response = await _thuongHieuService.Restore(id);
            return Json(new
            {
                success = response.Success,
                message = response.Message
            });
        }
    }
}
