using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;
using Microsoft.AspNetCore.Http;

namespace AuthDemo.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var username = HttpContext.Session.GetString("TenDangNhap");
        if (string.IsNullOrEmpty(username))
        {
            // Chưa đăng nhập, chuyển về trang Login
            return RedirectToAction("Login", "Account");
        }
        // Đã đăng nhập, chuyển về trang HomeAdmin
        return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
