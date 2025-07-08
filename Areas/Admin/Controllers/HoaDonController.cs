using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext db;
        public HoaDonController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
} 