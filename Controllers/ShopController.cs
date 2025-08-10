using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace AuthDemo.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShopController> _logger;
        public ShopController(ApplicationDbContext context, ILogger<ShopController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // show sp 
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page ?? 1;

            var products = _context.Giays
                .Where(g => g.ChiTietGiays.Any())
                .Select(g => new ProductViewModel
                {
                    ShoeID = g.ShoeID,
                    TenGiay = g.TenGiay,
                    AnhDaiDien = g.AnhDaiDien,
                    GiaThapNhat = g.ChiTietGiays.Min(ct => ct.Gia)
                })
                .ToList();

            var pagedProducts = new PagedList<ProductViewModel>(products, pageNumber, pageSize);

            // Truy vấn danh sách bộ lọc từ DB
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            ViewBag.MauSacs = _context.MauSacs.ToList();
            ViewBag.KichThuocs = _context.KichThuocs.ToList();

            ViewBag.MinPrice = _context.ChiTietGiays.Any() ? _context.ChiTietGiays.Min(c => c.Gia) : 0;
            ViewBag.MaxPrice = _context.ChiTietGiays.Any() ? _context.ChiTietGiays.Max(c => c.Gia) : 1000000;

            return View(pagedProducts);
        }


        [HttpPost]
        public IActionResult FilterProducts(List<Guid> danhMucIds, List<Guid> mauSacIds, List<Guid> kichThuocIds, int? minPrice, int? maxPrice)
        {
            var filtered = _context.ChiTietGiays
                .Include(ct => ct.Giay)
                .Where(ct => ct.SoLuong > 0);

            if (danhMucIds != null && danhMucIds.Any())
                filtered = filtered.Where(ct => danhMucIds.Contains(ct.DanhMuc.CategoryID));

            if (mauSacIds != null && mauSacIds.Any())
                filtered = filtered.Where(ct => mauSacIds.Contains(ct.MauSac.ColorID));

            if (kichThuocIds != null && kichThuocIds.Any())
                filtered = filtered.Where(ct => kichThuocIds.Contains(ct.KichThuoc.SizeID));

            if (minPrice.HasValue)
                filtered = filtered.Where(ct => ct.Gia >= minPrice.Value);

            if (maxPrice.HasValue)
                filtered = filtered.Where(ct => ct.Gia <= maxPrice.Value);

            var products = filtered
                .GroupBy(ct => ct.ShoeID)
                .Select(g => new ProductViewModel
                {
                    ShoeID = g.First().ShoeID,
                    TenGiay = g.First().Giay.TenGiay,
                    AnhDaiDien = g.First().Giay.AnhDaiDien,
                    GiaThapNhat = g.Min(x => x.Gia)
                })
                .ToList();

            return PartialView("_ProductListPartial", products);
        }

    }
}
