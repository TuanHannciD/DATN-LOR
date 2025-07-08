using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;
using AuthDemo.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation($"Login attempt for username: {model.TenDangNhap}");
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _context.NguoiDungs
                .Include(u => u.VaiTroNguoiDungs)
                    .ThenInclude(vtnd => vtnd.VaiTro)
                .FirstOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);

            if (user == null)
            {
                // User không tồn tại
                _logger.LogWarning($"User not found: {model.TenDangNhap}");
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại");
                return View(model);
            }

            var isAdmin = user.VaiTroNguoiDungs
                .Any(x => x.VaiTro != null && x.VaiTro.TenVaiTro.ToLower() == "admin");

            if (!isAdmin)
            {
                // Không cho đăng nhập
                _logger.LogWarning($"User {model.TenDangNhap} không phải Admin, bị chặn đăng nhập.");
                ModelState.AddModelError("", "Chỉ Admin mới được phép đăng nhập.");
                return View(model);
            }

            var passwordHash = HashPassword(model.MatKhau);
            _logger.LogInformation($"Password check - Input hash: {passwordHash}, DB hash: {user.MatKhau}");
            
            if (user.MatKhau != passwordHash)
            {
                _logger.LogWarning($"Password mismatch for user: {model.TenDangNhap}");
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View(model);
            }

            // Lấy danh sách vai trò, luôn non-null
            List<string?> tenVaiTroList;
            if (user.VaiTroNguoiDungs != null)
            {
                tenVaiTroList = user.VaiTroNguoiDungs
                    .Select(x => x.VaiTro?.TenVaiTro)
                    .ToList();
            }
            else
            {
                tenVaiTroList = new List<string?>();
            }

            var tenVaiTroLower = tenVaiTroList.Select(vt => vt?.ToLower()).ToList();

            if (!tenVaiTroLower.Contains("admin"))
            {
                _logger.LogWarning($"User {model.TenDangNhap} không phải Admin, bị chặn đăng nhập.");
                ModelState.AddModelError("", "Chỉ Admin mới được phép đăng nhập.");
                return View(model);
            }

            HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
            HttpContext.Session.SetString("VaiTro", string.Join(",", tenVaiTroList));

            _logger.LogInformation($"User logged in successfully: {user.TenDangNhap}, Role: {string.Join(",", tenVaiTroList)}");

            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
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

            if (tenVaiTroList.Any(vt => vt?.ToLower() == "admin" || vt?.ToLower() == "quản lý"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
} 