using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;
using System.Linq;
using AuthDemo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthDemo.Areas.Admin.Services;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;
using System.Threading.Tasks;
using static AuthDemo.Models.ViewModels.VMCHUNG;
using static AuthDemo.Models.ViewModels.GiayVM;
using AuthDemo.Models.Enums;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietGiayController : Controller
    {
        private readonly IChiTietGiayService _chiTietGiayService;
        private readonly IGiayService _giayService;
        private readonly IChatLieuService _chatLieuService;
        private readonly IThuongHieuService _thuongHieuService;
        private readonly IDanhMucService _danhMucService;
        private readonly ApplicationDbContext _context;

        public ChiTietGiayController(ApplicationDbContext context
        , IChiTietGiayService chiTietGiayService
        , IGiayService giayService
        , IChatLieuService chatLieuService
        , IDanhMucService danhMucService
        , IThuongHieuService thuongHieuService)

        {
            _context = context;
            _chiTietGiayService = chiTietGiayService;
            _giayService = giayService;
            _chatLieuService = chatLieuService;
            _danhMucService = danhMucService;
            _thuongHieuService = thuongHieuService;
        }
        [HttpGet]
        [Route("Admin/ChiTietGiay/GetSelectLists")]
        public IActionResult GetSelectLists()
        {
            var shoeList = _context.Giays
                .Where(g => !g.IsDelete) // chỉ lấy những sản phẩm chưa bị xóa
                .Select(g => new { id = g.ShoeID, name = g.TenGiay })
                .ToList();

            var sizeList = _context.KichThuocs
                .Where(s => !s.IsDelete)
                .Select(s => new { id = s.SizeID, name = s.TenKichThuoc })
                .ToList();

            var colorList = _context.MauSacs
                .Where(c => !c.IsDelete)
                .Select(c => new { id = c.ColorID, name = c.TenMau })
                .ToList();

            var materialList = _context.ChatLieus
                .Where(m => !m.IsDelete)
                .Select(m => new { id = m.MaterialID, name = m.TenChatLieu })
                .ToList();

            var brandList = _context.ThuongHieus
                .Where(b => !b.IsDelete)
                .Select(b => new { id = b.BrandID, name = b.TenThuongHieu })
                .ToList();

            var categoryList = _context.DanhMucs
                .Where(d => !d.IsDelete)
                .Select(d => new { id = d.CategoryID, name = d.TenDanhMuc })
                .ToList();

            return Ok(new
            {
                shoeList,
                sizeList,
                colorList,
                materialList,
                brandList,
                categoryList
            });
        }


        public async Task<IActionResult> Index()
        {
            var response = await _chiTietGiayService.GetAllIndexVMAsync(); // giả sử sync và trả về ApiResponse
            if (!response.Success)
            {
                // Lưu message vào TempData
                TempData["ToastMessage"] = response.Message;
                TempData["ToastType"] = response.Success;
                return View(new List<IndexVM>());
            }

            return View(response.Data); // Data là IEnumerable<ChiTietGiayVM.IndexVM>
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.GiayList = new SelectList(_context.Giays, "ShoeID", "TenGiay");
            ViewBag.SizeList = new SelectList(_context.KichThuocs, "SizeID", "TenKichThuoc");
            ViewBag.ColorList = new SelectList(_context.MauSacs, "ColorID", "TenMau");
            ViewBag.MaterialList = new SelectList(_context.ChatLieus, "MaterialID", "TenChatLieu");
            ViewBag.BrandList = new SelectList(_context.ThuongHieus, "BrandID", "TenThuongHieu");
            ViewBag.CategoryList = new SelectList(_context.DanhMucs, "CategoryID", "TenDanhMuc");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, errors });
            }

            var response = await _chiTietGiayService.Add(model);

            if (!response.Success)
            {
                return Json(new { success = false, message = response.Message });
            }

            return Json(new { success = true, message = response.Message });
        }

        // Tách hàm load SelectList cho gọn
        private void LoadSelectLists(CreateVM model)
        {
            ViewBag.GiayList = new SelectList(_context.Giays, "ShoeID", "TenGiay", model.ShoeID);
            ViewBag.SizeList = new SelectList(_context.KichThuocs, "SizeID", "TenKichThuoc", model.SizeIDs);
            ViewBag.ColorList = new SelectList(_context.MauSacs, "ColorID", "TenMau", model.ColorIDs);
            ViewBag.MaterialList = new SelectList(_context.ChatLieus, "MaterialID", "TenChatLieu", model.MaterialID);
            ViewBag.BrandList = new SelectList(_context.ThuongHieus, "BrandID", "TenThuongHieu", model.BrandID);
            ViewBag.CategoryList = new SelectList(_context.DanhMucs, "CategoryID", "TenDanhMuc", model.CategoryID);
        }


        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var ct = _context.ChiTietGiays.Find(id);
            if (ct == null) return NotFound();

            var giay = _context.Giays.Find(ct.ShoeID);
            var tenGiay = giay?.TenGiay;

            var vm = new EditVM
            {
                ShoeDetailID = ct.ShoeDetailID,
                TenGiay = tenGiay,
                Gia = ct.Gia,
                SoLuong = ct.SoLuong,
                ShoeID = ct.ShoeID,
                SizeID = ct.SizeID,
                ColorID = ct.ColorID,
                MaterialID = ct.MaterialID,
                BrandID = ct.BrandID,
                CategoryID = ct.CategoryID
                // map thêm các trường khác nếu cần
            };


            return Ok(vm);
        }



        [HttpPost]
        public IActionResult Update([FromBody] EditVM model)
        {
            var response = _chiTietGiayService.Update(model);
            if (response.Success)
            {
                return Json(new
                {
                    success = true,
                    message = response.Message
                });
            }
            else
            {
                ModelState.AddModelError("", response.Message);
            }
            return Json(new
            {
                success = false,
                message = response.Message
            });
        }

        public IActionResult Delete(Guid id)
        {
            var response = _chiTietGiayService.Delete(id);
            // Lưu message vào TempData
            TempData["ToastMessage"] = response.Message;
            TempData["ToastType"] = response.Success;

            return RedirectToAction("Index"); // hoặc về trang hiện tại
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> QuickAdd([FromBody] QuickAddVM quick)
        {
            if (quick.TenChatLieu == null && quick.TenDanhMuc == null && quick.TenGiay == null && quick.TenHang == null)
                return Json(new { success = false, message = "Vui lòng nhập thông tin cần thêm" });

            //  Thêm chất liệu
            if (!string.IsNullOrEmpty(quick.TenChatLieu))
            {
                var createVM = new CreateChatLieu { Ten = quick.TenChatLieu };
                var result = await _chatLieuService.AddAsync(createVM);

                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }
            // Thêm danh mục
            if (!string.IsNullOrEmpty(quick.TenDanhMuc))
            {
                var createVM = new CreateDanhMuc { Ten = quick.TenDanhMuc, MoTa = "Tạo nhanh" };
                var result = await _danhMucService.AddAsync(createVM);

                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }
            //  Thêm hãng
            if (!string.IsNullOrEmpty(quick.TenHang))
            {
                var createVM = new CreateHangSanXuat { Ten = quick.TenHang };
                var result = await _thuongHieuService.AddAsync(createVM);

                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }
            // Thêm giày
            if (!string.IsNullOrEmpty(quick.TenGiay))
            {
                var createVM = new GiayCreate { TenGiay = quick.TenGiay, MoTa = "Thêm nhanh", TrangThai = true };
                var result = await _giayService.AddAsync(createVM);

                return Json(new
                {
                    success = result.Success,
                    message = result.Message,
                    data = result.Data
                });
            }


            // TODO: Tương tự cho Giày, Hãng, DanhMục
            return Json(new { success = false, message = "Loại dữ liệu chưa được hỗ trợ" });
        }

    }

}
