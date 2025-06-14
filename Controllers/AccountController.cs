using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;
using AuthDemo.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AuthDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại");
                return View();
            }

            var passwordHash = HashPassword(password);
            if (user.Password != passwordHash)
            {
                ModelState.AddModelError("", "Mật khẩu không đúng");
                return View();
            }

            // Lưu thông tin đăng nhập vào session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
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
                model.Role = "Khách Hàng";
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