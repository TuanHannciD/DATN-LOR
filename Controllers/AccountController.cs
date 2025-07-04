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
        public async Task<IActionResult> Login(string username, string password)
        {
            _logger.LogInformation($"Login attempt for username: {username}");
            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin");
                return View();
            }

            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                _logger.LogWarning($"User not found: {username}");
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại");
                return View();
            }

            var passwordHash = HashPassword(password);
            _logger.LogInformation($"Password check - Input hash: {passwordHash}, DB hash: {user.Password}");
            
            if (user.Password != passwordHash)
            {
                _logger.LogWarning($"Password mismatch for user: {username}");
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View();
            }

            // PHÂN QUYỀN: Chỉ cho phép Admin đăng nhập
            if (user.Role == null || user.Role.Name.ToLower() != "admin")
            {
                _logger.LogWarning($"User {username} không phải Admin, bị chặn đăng nhập.");
                ModelState.AddModelError("", "Chỉ Admin mới được phép đăng nhập.");
                return View();
            }

            // Đăng nhập thành công, lưu session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role?.Name ?? "");

            _logger.LogInformation($"User logged in successfully: {user.Username}, Role: {user.Role?.Name}");

            // Chuyển hướng vào trang Admin
            return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                    return View(model);
                }

                model.Password = HashPassword(model.Password);
                model.RoleId = 1; // 1 là Id của Role "User" hoặc "Khách Hàng"
                _context.Users.Add(model);
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
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            var result = "Danh sách users:\n";
            
            foreach (var user in users)
            {
                result += $"Username: {user.Username}, Role: {user.Role?.Name}, Password: {user.Password}\n";
            }
            
            return Content(result);
        }

        // Action để test redirect
        public IActionResult TestRedirect()
        {
            return RedirectToAction("Index", "Home");
        }

        // Action để test login trực tiếp
        public async Task<IActionResult> TestLogin(string username)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                return Content($"User {username} not found");
            }

            // Lưu session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role?.Name ?? "");

            if (user.Role?.Name.ToLower() == "admin" || user.Role?.Name.ToLower() == "quản lý")
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