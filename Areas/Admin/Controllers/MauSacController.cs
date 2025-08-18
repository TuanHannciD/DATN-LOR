using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;

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
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MauSac model)
        {
            if (ModelState.IsValid)
            {
                _mauSacService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
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
        public IActionResult Edit(MauSac model)
        {
            if (ModelState.IsValid)
            {
                _mauSacService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _mauSacService.Delete(id);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }
    }
}
