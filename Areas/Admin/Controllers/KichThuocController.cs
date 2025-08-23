using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KichThuocController : Controller
    {
        private readonly IKichThuocService _sizeService;
        public KichThuocController(IKichThuocService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _sizeService.GetAll();
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
                var list = await _sizeService.GetAllDelete();
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateKichThuoc model)
        {
            var response = await _sizeService.AddAsync(model);
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var sz = _sizeService.GetById(id);
            if (sz == null) return NotFound();
            return View(sz);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KichThuoc model)
        {
            if (ModelState.IsValid)
            {
                _sizeService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _sizeService.Delete(id);
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
            var response = await _sizeService.Restore(id);
            return Json(new
            {
                success = response.Success,
                message = response.Message
            });
        }
    }
}
