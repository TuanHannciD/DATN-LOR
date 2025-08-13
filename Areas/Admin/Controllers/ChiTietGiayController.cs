using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;
using System.Linq;
using AuthDemo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using AuthDemo.Areas.Admin.Services;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietGiayController : Controller
    {
        private readonly IChiTietGiayService _chiTietGiayService;
        private readonly ApplicationDbContext _context;

        public ChiTietGiayController(ApplicationDbContext context, IChiTietGiayService chiTietGiayService)

        {
            _context = context;
            _chiTietGiayService = chiTietGiayService;
        }
        [HttpGet]
        [Route("Admin/ChiTietGiay/GetSelectLists")]
        public IActionResult GetSelectLists()
        {
            var shoeList = _context.Giays.Select(g => new { id = g.ShoeID, name = g.TenGiay }).ToList();
            var sizeList = _context.KichThuocs.Select(s => new { id = s.SizeID, name = s.TenKichThuoc }).ToList();
            var colorList = _context.MauSacs.Select(c => new { id = c.ColorID, name = c.TenMau }).ToList();
            var materialList = _context.ChatLieus.Select(m => new { id = m.MaterialID, name = m.TenChatLieu }).ToList();
            var brandList = _context.ThuongHieus.Select(b => new { id = b.BrandID, name = b.TenThuongHieu }).ToList();
            var categoryList = _context.DanhMucs.Select(d => new { id = d.CategoryID, name = d.TenDanhMuc }).ToList();

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

        public IActionResult Index()
        {
            var viewModelList = _chiTietGiayService is ChiTietGiayService service
                ? service.GetAllIndexVM()
                : new List<ChiTietGiayVM.IndexVM>();
            return View(viewModelList);
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Tạo mới đối tượng ChiTietGiay từ EditVM
                    var entity = new ChiTietGiay
                    {
                        ShoeDetailID = Guid.NewGuid(),
                        ShoeID = model.ShoeID,
                        SizeID = model.SizeID,
                        ColorID = model.ColorID,
                        MaterialID = model.MaterialID,
                        BrandID = model.BrandID,
                        CategoryID = model.CategoryID,
                        SoLuong = model.SoLuong,
                        Gia = model.Gia
                    };

                    _context.ChiTietGiays.Add(entity);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm chi tiết giày: " + ex.Message);
                }
            }
            // Nạp lại các ViewBag để tránh lỗi khi reload form
            ViewBag.GiayList = new SelectList(_context.Giays, "ShoeID", "TenGiay", model.ShoeID);
            ViewBag.SizeList = new SelectList(_context.KichThuocs, "SizeID", "TenKichThuoc", model.SizeID);
            ViewBag.ColorList = new SelectList(_context.MauSacs, "ColorID", "TenMau", model.ColorID);
            ViewBag.MaterialList = new SelectList(_context.ChatLieus, "MaterialID", "TenChatLieu", model.MaterialID);
            ViewBag.BrandList = new SelectList(_context.ThuongHieus, "BrandID", "TenThuongHieu", model.BrandID);
            ViewBag.CategoryList = new SelectList(_context.DanhMucs, "CategoryID", "TenDanhMuc", model.CategoryID);
            return View(model);
        }

        [HttpGet]
        public IActionResult GetById(Guid id)
        {
            var ct = _context.ChiTietGiays.Find(id);
            if (ct == null) return NotFound();

            var giay = _context.Giays.Find(ct.ShoeID);
            var tenGiay = giay?.TenGiay;

            var vm = new ChiTietGiayVM.EditVM
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
        public IActionResult Update([FromBody]EditVM model)
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
            var ct = _context.ChiTietGiays.Find(id);
            if (ct != null)
            {
                _context.ChiTietGiays.Remove(ct);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

}
