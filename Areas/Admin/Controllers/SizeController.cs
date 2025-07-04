using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using System;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public IActionResult Index()
        {
            var list = _sizeService.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Size model)
        {
            if (ModelState.IsValid)
            {
                _sizeService.Add(model);
                return RedirectToAction("Index");
            }
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
        public IActionResult Edit(Size model)
        {
            if (ModelState.IsValid)
            {
                _sizeService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            _sizeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
} 