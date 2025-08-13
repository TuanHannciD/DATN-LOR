using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoaDonController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHoaDonService _hoaDonService;
        public HoaDonController(ApplicationDbContext context, IHoaDonService hoaDonService)
        {
            db = context;
            _hoaDonService = hoaDonService;
        }
        public IActionResult Index()
        {
            var hoaDons = _hoaDonService.GetAllHoaDon();
            return View(hoaDons);
        }
        public IActionResult Create()
        {
            // Chưa có logic cụ thể, chỉ để giữ nguyên cấu trúc
            return View();
        }
        [HttpPost]
        [Route("Admin/HoaDon/CreateHoaDon")]
        public async Task<IActionResult> CreateHoaDon([FromBody] CreateHoaDonVM createHoaDonVM)
        {
            var tenDangNhap = HttpContext.Session.GetString("TenDangNhap");
            if (string.IsNullOrEmpty(tenDangNhap))
            {
                return BadRequest(new { message = "Không tìm thấy thông tin đăng nhập người dùng." });
            }

            var result = await _hoaDonService.CreateHoaDon(createHoaDonVM, tenDangNhap);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error ?? "Tạo hóa đơn thất bại." });
            }

            return Ok(new
            {
                message = "Hóa đơn đã được tạo thành công.",
                hoaDon = result.Data
            });
        }


    }
}