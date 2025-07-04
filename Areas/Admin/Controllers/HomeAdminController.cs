using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
} 