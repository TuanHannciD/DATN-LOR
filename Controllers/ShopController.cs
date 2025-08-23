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
        public IActionResult Index(int? page, string searchString)
        {
            int pageSize = 9;
            int pagenumber = page == null || page < 0 ? 1 : page.Value;
            var query = _context.Giays
        .Where(g => g.ChiTietGiays.Any());

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(g => g.TenGiay.ToLower().Contains(searchString.ToLower()));
            }

            var product = query
    .Select(g => new ProductViewModel
    {
        ShoeID = g.ShoeID,
        TenGiay = g.TenGiay,
        AnhDaiDien = g.ChiTietGiays
                        .OrderBy(ct => ct.ShoeDetailID)
                        .Select(ct => ct.AnhGiays
                            .OrderBy(a => a.ShoeDetailID)
                            .Select(a => a.DuongDanAnh)
                            .FirstOrDefault())
                        .FirstOrDefault(path => path != null),
        GiaThapNhat = g.ChiTietGiays.Min(ct => ct.Gia)
    })
    .ToList();

            var pagedProducts = new PagedList<ProductViewModel>(product, pagenumber, pageSize);

            // Truy vấn danh sách bộ lọc từ DB
            ViewBag.DanhMucs = _context.DanhMucs.ToList();
            ViewBag.MauSacs = _context.MauSacs.ToList();
            ViewBag.KichThuocs = _context.KichThuocs.ToList();

            ViewBag.MinPrice = _context.ChiTietGiays.Any() ? _context.ChiTietGiays.Min(c => c.Gia) : 0;
            ViewBag.MaxPrice = _context.ChiTietGiays.Any() ? _context.ChiTietGiays.Max(c => c.Gia) : 10000000;
            ViewBag.SearchString = searchString;
            return View(pagedProducts);
        }
        [HttpGet]
        public JsonResult GetSearchSuggestions(string term)
        {
            var suggestions = _context.Giays
                .Where(g => g.TenGiay.ToLower().Contains(term.ToLower()))
                .Where(g => g.ChiTietGiays.Any())
                .Select(g => new { label = g.TenGiay, value = g.TenGiay })
                .Take(5) // Giới hạn 5 gợi ý
                .ToList();

            return Json(suggestions);
        }

        [HttpPost]
        public IActionResult FilterProducts(List<Guid> danhMucIds, List<Guid> mauSacIds, List<Guid> kichThuocIds, int? minPrice, int? maxPrice)
        {
            var filtered = _context.ChiTietGiays
                .Include(ct => ct.Giay)
                .Include(ct => ct.AnhGiays)
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
                    AnhDaiDien = g.SelectMany(ct => ct.AnhGiays)
                          .Select(a => a.DuongDanAnh)
                          .FirstOrDefault(path => path != null)
                          ?? g.First().Giay.AnhDaiDien,
                    GiaThapNhat = g.Min(x => x.Gia)
                })
                .ToList();

            return PartialView("_ProductListPartial", products);
        }

    }
}
