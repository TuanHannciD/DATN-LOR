using Microsoft.AspNetCore.Mvc;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models;
using System;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HangSXController : Controller
    {
        private readonly IHangSXService _hangSXService;
        public HangSXController(IHangSXService hangSXService)
        {
            _hangSXService = hangSXService;
        }

        public IActionResult Index()
        {
            var list = _hangSXService.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangSX model)
        {
            if (ModelState.IsValid)
            {
                _hangSXService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var hang = _hangSXService.GetById(id);
            if (hang == null) return NotFound();
            return View(hang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HangSX model)
        {
            if (ModelState.IsValid)
            {
                _hangSXService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            _hangSXService.Delete(id);
            return RedirectToAction("Index");
        }
    }
} 