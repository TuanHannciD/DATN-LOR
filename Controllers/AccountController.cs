using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;
using AuthDemo.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using AuthDemo.Helpers;

namespace AuthDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ApplicationDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthDemo.Models.ViewModels.LoginViewModel model)
        {
            // Ghi log: Bắt đầu đăng nhập
            _logger.LogInformation($"Login attempt for username: {model.TenDangNhap}");
            // Kiểm tra dữ liệu đầu vào hợp lệ
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Tìm user theo tên đăng nhập, kèm theo các vai trò
            var user = await _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                    .ThenInclude(vtnd => vtnd.VaiTro)
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);
            if (user == null)
            {
                // Không tìm thấy user
                _logger.LogWarning($"User not found: {model.TenDangNhap}");
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại");
                return View(model);
            }
            // Hash mật khẩu nhập vào và so sánh với DB
            var passwordHash = HashPassword(model.MatKhau);
            _logger.LogInformation($"Password check - Input hash: {passwordHash}, DB hash: {user.MatKhau}");
            if (user.MatKhau != passwordHash)
            {
                // Sai mật khẩu
                _logger.LogWarning($"Password mismatch for user: {model.TenDangNhap}");
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View(model);
            }
            // Lấy danh sách tên vai trò của user (có thể chứa null)
            var tenVaiTroList = user.VaiTroNguoiDungs?
                    .Select(x => x.VaiTro?.TenVaiTro)
                .ToList() ?? new List<string?>();
            // Lưu thông tin đăng nhập vào session
            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", string.Join(",", tenVaiTroList));
            // Khởi tạo giỏ hàng nếu chưa có
            var cart = await _context.GioHangs.FirstOrDefaultAsync(a => a.UserID == user.UserID);
            if (cart == null)
            {
                cart = new GioHang
                {
                    CartID = Guid.NewGuid(),
                    UserID = user.UserID,
                    NguoiTao = "system",
                    NguoiCapNhat = "system",
                    NgayTao = DateTime.Now,
                    NgayCapNhat = DateTime.Now
                };
                _context.GioHangs.Add(cart);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("CartID", cart.CartID.ToString());
            }
            else
            {
                HttpContext.Session.SetString("CartID", cart.CartID.ToString());
            }

            // Loại bỏ các phần tử null khỏi danh sách vai trò để kiểm tra
            var vaiTroListNonNull = tenVaiTroList.Where(x => x != null).Cast<string>();
            // Nếu user có cả vai trò admin và user, chuyển sang trang chọn vai trò
            if (RoleHelper.HasBothAdminAndUser(vaiTroListNonNull))
            {
                TempData["RoleChoice"] = "both";
                return RedirectToAction("ChooseRole");
            }
            // Nếu chỉ có admin, chuyển thẳng vào trang admin
            if (RoleHelper.IsAdmin(vaiTroListNonNull))
            {
            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
            }
            // Nếu chỉ có user, chuyển vào trang người dùng
            if (RoleHelper.IsUser(vaiTroListNonNull))
            {
                // Chuyển về Home/Index (Razor View)
                return RedirectToAction("Index", "Home");
            }
            // Nếu không có quyền hợp lệ, báo lỗi
            ModelState.AddModelError("", "Bạn không có quyền truy cập.");
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(NguoiDung model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.NguoiDungs.AnyAsync(u => u.TenDangNhap == model.TenDangNhap))
                {
                    ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại");
                    return View(model);
                }

                model.MatKhau = HashPassword(model.MatKhau);
                _context.NguoiDungs.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Action để debug - tạo hash cho mật khẩu
        public IActionResult DebugHash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return Content("Vui lòng nhập mật khẩu: /Account/DebugHash?password=yourpassword");
            }
            
            var hash = HashPassword(password);
            return Content($"Password: {password}\nHash: {hash}");
        }

        // Action để xem danh sách user trong database
        public async Task<IActionResult> DebugUsers()
        {
            var users = await _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                    .ThenInclude(vtnd => vtnd.VaiTro)
                .ToListAsync();
            var result = "Danh sách users:\n";
            
            foreach (var user in users)
            {
                var vaiTros = user.VaiTroNguoiDungs?
                    .Select(x => x.VaiTro?.TenVaiTro)
                    .ToList() ?? new List<string?>();
                result += $"TenDangNhap: {user.TenDangNhap}, VaiTro: {string.Join(",", vaiTros)}, MatKhau: {user.MatKhau}\n";
            }
            
            return Content(result);
        }

        // Action để test redirect
        public IActionResult TestRedirect()
        {
            return RedirectToAction("Index", "Home");
        }

        // Action để test login trực tiếp
        public async Task<IActionResult> TestLogin(string tenDangNhap)
        {
            var user = await _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                    .ThenInclude(vtnd => vtnd.VaiTro)
                .FirstOrDefaultAsync(u => u.TenDangNhap == tenDangNhap);
            if (user == null)
            {
                return Content($"User {tenDangNhap} not found");
            }

            var tenVaiTroList = user.VaiTroNguoiDungs?
                .Select(x => x.VaiTro?.TenVaiTro)
                .ToList() ?? new List<string?>();

            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", string.Join(",", tenVaiTroList));

            var vaiTroListNonNull = tenVaiTroList.Where(x => x != null).Cast<string>();
            if (tenVaiTroList.Any(vt => vt?.ToLower() == "admin" || vt?.ToLower() == "quản lý"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult ChooseRole()
        {
            // View sẽ hiện 2 nút: Vào Admin hoặc Vào Home
            return View();
        }

        [HttpPost]
        public IActionResult ChooseRole(string role)
        {
            if (role == "admin")
                return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
            else if (role == "user")
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public IActionResult UserProfile()
        {
            var check = HttpContext.Session.GetString("TenDangNhap");
            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;
           

            //if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
            //{
            //    return NotFound();
            //}

            var user = _context.NguoiDungs.FirstOrDefault(a => a.UserID == userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public IActionResult EditProfile()
        {
            var check = HttpContext.Session.GetString("TenDangNhap");
            var userId = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;
            
            //if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
            //{
            //    return NotFound();
            //}

            var user = _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                .FirstOrDefault(a => a.UserID == userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(NguoiDung model, IFormFile? avatarFile)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid during EditProfile for UserID: {UserID}", model?.UserID ?? Guid.Empty);
                ViewBag.Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return View(model);
            }

            var user = _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                .FirstOrDefault(u => u.UserID == model.UserID);

            if (user == null)
            {
                _logger.LogWarning("User not found with UserID: {UserID}", model.UserID);
                return NotFound();
            }

            _logger.LogInformation("Updating user with UserID: {UserID}", model.UserID);
            user.HoTen = model.HoTen;
            user.Email = model.Email;
            user.SoDienThoai = model.SoDienThoai;

            // Xử lý upload ảnh
            if (avatarFile != null && avatarFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "avatar");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatarFile.CopyToAsync(stream);
                    }
                    user.AnhDaiDien = fileName; // Lưu tên file vào SQL
                    _logger.LogInformation("Avatar uploaded successfully for UserID: {UserID}, File: {FileName}", model.UserID, fileName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error uploading avatar for UserID: {UserID}", model.UserID);
                    ViewBag.Errors = new List<string> { $"Lỗi upload ảnh: {ex.Message}" };
                    return View(model);
                }
            }

            // Điền các trường từ ThongTinChung
            user.NguoiTao = user.NguoiTao ?? HttpContext.Session.GetString("TenDangNhap") ?? "system";
            user.NguoiCapNhat = HttpContext.Session.GetString("TenDangNhap") ?? "system";
            user.NgayTao = user.NgayTao == default ? DateTime.Now : user.NgayTao;
            user.NgayCapNhat = DateTime.Now;

            // Xử lý VaiTroNguoiDungs
            if (user.VaiTroNguoiDungs == null)
            {
                user.VaiTroNguoiDungs = new List<VaiTroNguoiDung>();
            }

            try
            {
                _context.Update(user);
                _context.SaveChanges();
                _logger.LogInformation("User {UserID} updated successfully.", model.UserID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes for UserID: {UserID}", model.UserID);
                ViewBag.Errors = new List<string> { $"Lỗi lưu dữ liệu: {ex.Message}" };
                return View(model);
            }

            return RedirectToAction("UserProfile");
        } 
    }
} 