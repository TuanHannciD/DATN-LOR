using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Models.ViewModels;
using AuthDemo.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            ViewBag.TrangThaiDonHangList = Enum.GetValues(typeof(TrangThaiHoaDon))
                .Cast<TrangThaiHoaDon>()
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.GetDisplayName() // Nếu có DisplayAttribute
                }).ToList();

            ViewBag.HinhThucThanhToanList = Enum.GetValues(typeof(PhuongThucThanhToan))
                .Cast<PhuongThucThanhToan>()
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.GetDisplayName()
                }).ToList();

            return View(); // Không truyền Model trực tiếp, dữ liệu load bằng AJAX
        }

        // API trả JSON danh sách hóa đơn theo filter
        [HttpGet]
        public IActionResult GetHoaDons(DateTime? startDate,
         DateTime? endDate,
        string trangThai = "",
        string hinhThuc = "",
        string phone = "",
         string idFilter = "",
         string nameFilter = "",
         bool? trangThaiTT = null,
         string nameCreateFilter = "",
         string tongTienFilter = ""
        )
        {
            var hoaDons = _hoaDonService.GetAllHoaDon();


            if (startDate.HasValue)
                hoaDons = hoaDons.Where(h => h.NgayTao >= startDate.Value).ToList();

            if (endDate.HasValue)
                hoaDons = hoaDons.Where(h => h.NgayTao <= endDate.Value).ToList();


            if (!string.IsNullOrEmpty(trangThai))
                if (Enum.TryParse<TrangThaiHoaDon>(trangThai, out var trangthaiEnum))
                {
                    hoaDons = hoaDons.Where(h => h.TrangThaiHoaDon == trangthaiEnum).ToList();
                }


            if (!string.IsNullOrEmpty(hinhThuc))
            {
                if (Enum.TryParse<PhuongThucThanhToan>(hinhThuc, out var hinhThucEnum))
                {
                    hoaDons = hoaDons.Where(h => h.HinhThucThanhToan == hinhThucEnum).ToList();
                }
            }


            if (!string.IsNullOrEmpty(phone))
            {
                // Lọc số điện thoại bắt đầu với phone
                hoaDons = hoaDons
                    .Where(h => h.SoDienThoai != null && h.SoDienThoai.StartsWith(phone))
                    .ToList();
            }


            if (!string.IsNullOrEmpty(idFilter))
            {
                hoaDons = hoaDons
                    .Where(h => h.HoaDonID.ToString().StartsWith(idFilter))
                    .ToList();
            }
            if (!string.IsNullOrEmpty(nameCreateFilter))
            {
                hoaDons = hoaDons
                    .Where(h => !string.IsNullOrEmpty(h.NguoiTao) &&
                                h.NguoiTao.StartsWith(nameCreateFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            // if (!string.IsNullOrEmpty(nameFilter))
            // {
            //     var keywords = nameFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //     hoaDons = hoaDons
            //         .Where(h => keywords.All(k => h.TenKhachHang.Contains(k, StringComparison.OrdinalIgnoreCase)))
            //         .ToList();
            // }
            // loc tên theo phương pháp ADN


            if (!string.IsNullOrEmpty(nameFilter))
            {
                var keywords = nameFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                hoaDons = hoaDons
                    .Where(h => !string.IsNullOrEmpty(h.TenKhachHang) &&
                                keywords.All(k => h.TenKhachHang.Contains(k, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }



            if (trangThaiTT.HasValue)
            {
                hoaDons = hoaDons.Where(h => h.DaThanhToan == trangThaiTT.Value).ToList();
            }

            // Sắp xếp mới nhất -> cũ nhất
            hoaDons = hoaDons.OrderByDescending(h => h.NgayTao).ToList();

            return Json(hoaDons);
        }

        public IActionResult GetTrangThaiList()
        {
            var list = _hoaDonService.GetTrangThaiList();
            return Json(list);
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


        [HttpGet]
        public async Task<IActionResult> GetHoaDonChiTiet(Guid billID)
        {
            var hoaDon = await db.HoaDons
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.Giay)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.MauSac)
                .Include(h => h.ChiTietHoaDons)
                    .ThenInclude(c => c.ChiTietGiay)
                        .ThenInclude(ctg => ctg.KichThuoc)
                .FirstOrDefaultAsync(h => h.BillID == billID);

            if (hoaDon == null) return NotFound();

            var result = new
            {
                hoaDon.BillID,
                hoaDon.HoTen,
                hoaDon.Email,
                hoaDon.SoDienThoai,
                hoaDon.DiaChi,
                hoaDon.TongTien,
                hoaDon.TrangThai,
                trangThaiDisplay = hoaDon.TrangThai.GetDisplayName(),
                hoaDon.DaThanhToan,
                hoaDon.PhuongThucThanhToan,
                PhuongThucThanhToanDisplay = hoaDon.PhuongThucThanhToan.GetDisplayName(),
                PhuongThucVanChuyenDisplay = hoaDon.PhuongThucVanChuyen.GetDisplayName(),
                hoaDon.PhuongThucVanChuyen,
                hoaDon.GhiChu,
                hoaDon.NgayGiaoHang,
                ChiTietHoaDons = hoaDon.ChiTietHoaDons.Select(c => new
                {
                    c.BillDetailID,
                    c.SoLuong,
                    c.DonGia,
                    c.ChietKhauPhanTram,
                    c.ChietKhauTienMat,
                    c.IsTangKem,
                    TenGiay = c.ChiTietGiay?.Giay?.TenGiay ?? "Không tìm thấy tên giày",
                    MauSac = c.ChiTietGiay?.MauSac?.TenMau ?? "Không tìm thấy tên màu săc",
                    Size = c.ChiTietGiay?.KichThuoc?.TenKichThuoc ?? "Không tìm thấy tên kích thước"
                })
            };

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> XacnhanTienMat(bool confirmdone, Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return BadRequest(new { message = "ID đơn hàng không hợp lệ." });
            }
            if (!confirmdone)
            {
                return BadRequest(new { message = "Bạn chưa xác nhận đã thanh toán." });
            }
            var result = await _hoaDonService.XacnhanTienMat(confirmdone, orderId);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Error ?? "Cập nhật trạng thái thanh toán thất bại." });
            }
            return Ok(new { message = "Cập nhật trạng thái thanh toán thành công." });
        }

        public class HoaDonIdDto
        {
            public Guid HoaDonID { get; set; }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTrangThai([FromBody] HoaDonIdDto HoaDonID)
        {

            var result = await _hoaDonService.UpdateTrangThai(HoaDonID.HoaDonID);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(new { success = true, message = result });
        }

        [HttpPost]
        public async Task<IActionResult> HuyHoaDon(Guid id)
        {
            var result = await _hoaDonService.HuyHoaDon(id);

            if (!result.Success)
                return BadRequest(new { message = result.Message ?? "Hủy hóa đơn thất bại." });

            return Ok(new { message = result.Message, data = result.Data });
        }



    }
}
