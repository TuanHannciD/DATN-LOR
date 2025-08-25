using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChatLieuController : Controller
    {
        private readonly IChatLieuService _chatLieuService;
        public ChatLieuController(IChatLieuService chatLieuService)
        {
            _chatLieuService = chatLieuService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _chatLieuService.GetAll();
            // nếu lấy thất bại thì trả về danh sách rỗng
            if (!response.Success)
            {
                return View(new List<ChatLieu>());
            }

            // View nhận đúng IEnumerable<KichThuoc>
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDelete()
        {
            try
            {
                var list = await _chatLieuService.GetAllDelete();
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
        public async Task<IActionResult> Create(CreateChatLieu model)
        {
            var response = await _chatLieuService.AddAsync(model);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var cl = _chatLieuService.GetById(id);
            if (cl == null) return NotFound();
            return View(cl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChatLieu model)
        {

            var response = await _chatLieuService.Update(model);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _chatLieuService.Delete(id);
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
            var response = await _chatLieuService.Restore(id);
            return Json(new
            {
                success = response.Success,
                message = response.Message
            });
        }
    }
}
