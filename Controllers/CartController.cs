using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CartController> _logger;
        public CartController(ApplicationDbContext context, ILogger<CartController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var check = HttpContext.Session.GetString("TenDangNhap");
            if (string.IsNullOrEmpty(check))
            {
                return RedirectToAction("Login", "Account");
            }


            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;
            var cartId = _context.GioHangs.FirstOrDefault(a => a.UserID == userId).CartID;

            var cartItems = _context.ChiTietGioHangs
                .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.Giay)
                .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.MauSac)
                .Include(c => c.ChiTietGiay).ThenInclude(ct => ct.KichThuoc)
                .Where(c => c.CartID == cartId)
                .ToList();

            var warnings = new List<string>();

            foreach (var item in cartItems)
            {
                var stock = item.ChiTietGiay.SoLuong;
                if (stock == 0)
                {
                    warnings.Add($"Sản phẩm {item.ChiTietGiay.Giay.TenGiay} ({item.ChiTietGiay.MauSac.TenMau}, {item.ChiTietGiay.KichThuoc.TenKichThuoc}) đã hết hàng, nên đã bị xóa khỏi giỏ hàng");
                    _context.ChiTietGioHangs.Remove(item);
                }
                else if (item.SoLuong > stock)
                {
                    item.SoLuong = stock;
                    warnings.Add($"Sản phẩm {item.ChiTietGiay.Giay.TenGiay} ({item.ChiTietGiay.MauSac.TenMau}, {item.ChiTietGiay.KichThuoc.TenKichThuoc}) đã được giảm xuống còn {stock} do vượt quá tồn kho.");
                }
            }

            if (warnings.Any())
            {
                TempData["CartWarnings"] = string.Join("<br>", warnings);
                _context.SaveChanges();
            }

            return View(cartItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(Guid shoeDetailId, int newQuantity)
        {

            var check = HttpContext.Session.GetString("TenDangNhap");
            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;
            var cartId = _context.GioHangs.FirstOrDefault(a => a.UserID == userId).CartID;

            var cartItem = _context.ChiTietGioHangs
            .Include(c => c.ChiTietGiay)
            .ThenInclude(ct => ct.Giay)
            .Where(c => c.CartID == cartId)
            .FirstOrDefault(c => c.ShoeDetailID == shoeDetailId);

            if (cartItem == null)
                return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng." });

            var stockQuantity = cartItem.ChiTietGiay.SoLuong;

            if (newQuantity > stockQuantity)
            {
                return Json(new
                {
                    success = false,
                    message = $"Chỉ còn {stockQuantity} sản phẩm trong kho."
                });
            }

            cartItem.SoLuong = newQuantity;
            _context.SaveChanges();

            decimal newItemTotal = cartItem.SoLuong * cartItem.ChiTietGiay.Gia;

            return Json(new
            {
                success = true,
                newItemTotal = newItemTotal
            });
        }
        public IActionResult RemoveCart(Guid id)
        {
            var itemCart = _context.ChiTietGioHangs.Find(id);
            _context.ChiTietGioHangs.Remove(itemCart);
            _context.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
    }
}
