using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;
using System.Linq;
using AuthDemo.Data;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietGiayController : Controller
    {
        private readonly   IChiTietGiayService _chiTietGiayService;
        private readonly ApplicationDbContext _context;

        public ChiTietGiayController(ApplicationDbContext context ,IChiTietGiayService chiTietGiayService)
        {
            _context = context;
            _chiTietGiayService = chiTietGiayService;
        }

        public IActionResult Index()
        {
            // Lấy tất cả sản phẩm (giày)
            var giayList = _context.Giays.ToList();

            // Tạo danh sách ViewModel
            var viewModelList = giayList.Select(giay => new GiayFullInfoVM
            {
                Giay = giay,
                ChiTietGiays = _context.ChiTietGiays
                    .Where(ct => ct.ShoeID == giay.ShoeID)
                    .ToList()
            }).ToList();

            return View(viewModelList);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChiTietGiay model)
        {
            if (ModelState.IsValid)
            {
                _context.ChiTietGiays.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var ct = _context.ChiTietGiays.Find(id);
            if (ct == null) return NotFound();
            return View(ct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ChiTietGiay model)
        {
            if (ModelState.IsValid)
            {
                _context.ChiTietGiays.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
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