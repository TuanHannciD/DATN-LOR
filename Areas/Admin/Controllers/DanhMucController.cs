using System.Threading.Tasks;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using Microsoft.AspNetCore.Mvc;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DanhMucController : Controller
    {
        private readonly IDanhMucService _danhmucSV;
        public DanhMucController(IDanhMucService danhMucService)
        {
            _danhmucSV = danhMucService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _danhmucSV.GetAll();
            // thất bại trả ds rỗng 
            if (!response.Success)
            {
                return View(new List<DanhMuc>());
            }
            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDanhMuc createDanhMuc)
        {
            var response = await _danhmucSV.AddAsync(createDanhMuc);

            TempData["ToastMessage"] = response.Success ? "Thêm thành công" : "Thêm thất bại";
            TempData["ToastType"] = response.Success ? "success" : "error";

            if (response.Success)
            {
                // Chuyển hướng về Index nếu thêm thành công
                return RedirectToAction("Index", "DanhMuc", new { area = "Admin" });
            }

            // Nếu thêm thất bại thì ở lại form để nhập lại
            return View(createDanhMuc);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var cl = _danhmucSV.GetById(id);
            if (cl == null) return NotFound();
            return View(cl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(DanhMuc model)
        {
            _danhmucSV.Update(model);

            TempData["ToastMessage"] = "Cập nhật thành công";
            TempData["ToastType"] = "success";

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _danhmucSV.Delete(id);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;
            return RedirectToAction("Index");
        }
    }
}
