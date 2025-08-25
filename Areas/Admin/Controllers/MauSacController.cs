using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MauSacController : Controller
    {
        private readonly IMauSacService _mauSacService;
        public MauSacController(IMauSacService mauSacService)
        {
            _mauSacService = mauSacService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mauSacService.GetAll();
            // nếu lấy thất bại thì trả về danh sách rỗng
            if (!response.Success)
            {
                return View(new List<KichThuoc>());
            }

            // View nhận đúng IEnumerable<KichThuoc>
            return View(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDelete()
        {
            try
            {
                var list = await _mauSacService.GetAllDelete();
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMauSac model)
        {
            var response = await _mauSacService.AddAsync(model);
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var ms = _mauSacService.GetById(id);
            if (ms == null) return NotFound();
            return View(ms);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MauSac model)
        {
            var response = await _mauSacService.Update(model);
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mauSacService.Delete(id);
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
            var response = await _mauSacService.Restore(id);
            return Json(new
            {
                success = response.Success,
                message = response.Message
            });
        }
    }
}
