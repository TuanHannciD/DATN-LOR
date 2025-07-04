using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Data;
using AuthDemo.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 10;

        public UserManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Danh sách tài khoản
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            var query = _context.Users.Include(u => u.Role).Where(u => u.IsActive);
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Username.Contains(search) || (u.Role != null && u.Role.Name.Contains(search)));
            }
            int total = await query.CountAsync();
            var users = await query.OrderBy(u => u.Username)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            ViewBag.Total = total;
            ViewBag.Page = page;
            ViewBag.PageSize = PageSize;
            ViewBag.Search = search;
            return View(users);
        }

        // GET: Thêm tài khoản
        public IActionResult Create()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        // POST: Thêm tài khoản
        [HttpPost]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Users.AnyAsync(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại");
                    ViewBag.Roles = _context.Roles.ToList();
                    return View(model);
                }
                model.IsActive = true;
                model.Password = HashPassword(model.Password);
                _context.Users.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(model);
        }

        // GET: Sửa tài khoản
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == id);
            if (user == null) return NotFound();
            ViewBag.Roles = _context.Roles.ToList();
            return View("EditUser", user);
        }

        // POST: Sửa tài khoản
        [HttpPost]
        public async Task<IActionResult> EditUser(User model)
        {
            if (string.IsNullOrEmpty(model.Password))
                ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user == null) return NotFound();
                if (!string.IsNullOrEmpty(model.Password))
                    user.Password = HashPassword(model.Password);
                user.RoleId = model.RoleId;
                user.IsActive = model.IsActive;
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Roles = _context.Roles.ToList();
            return View(model);
        }

        // Xóa mềm tài khoản
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsActive = false;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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