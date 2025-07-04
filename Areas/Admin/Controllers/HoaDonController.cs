using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Models;

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
    }
} 