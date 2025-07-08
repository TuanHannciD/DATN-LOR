using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChiTietGiayController : Controller
    {
        private readonly IChiTietGiayService _spctService;
        public ChiTietGiayController(IChiTietGiayService spctService)
        {
            _spctService = spctService;
        }

        public IActionResult Index()
        {
            var list = _spctService.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ChiTietGiay model)
        {
            if (ModelState.IsValid)
            {
                _spctService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var ct = _spctService.GetById(id);
            if (ct == null) return NotFound();
            return View(ct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ChiTietGiay model)
        {
            if (ModelState.IsValid)
            {
                _spctService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            _spctService.Delete(id);
            return RedirectToAction("Index");
        }
    }
} 