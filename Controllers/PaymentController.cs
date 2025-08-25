using AuthDemo.Data;
using AuthDemo.Models.Enums;
using AuthDemo.Models.VnPay;
using AuthDemo.Services.VnPay;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAnTotNghiep2024.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext _context;
        private readonly IVnPayService _vnPayService;
        public PaymentController(ApplicationDbContext context,IVnPayService vnPayService)
        {
            _context = context;
            _vnPayService = vnPayService;
        }
        [HttpGet]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        [HttpGet]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (!response.Success)
            {
                return RedirectToAction("Index", "Cart");
            }

            var billId = Guid.ParseExact(response.OrderId, "N");
            var hoaDon = _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .FirstOrDefault(h => h.BillID == billId);

            if (hoaDon == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            if (response.VnPayResponseCode == "00") // ✅ Thanh toán thành công
            {
                foreach (var ct in hoaDon.ChiTietHoaDons)
                {
                    var spct = _context.ChiTietGiays.FirstOrDefault(s => s.ShoeDetailID == ct.ShoeDetailID);
                    if (spct != null)
                    {
                        spct.SoLuong -= ct.SoLuong; // 👉 trừ kho lúc này
                    }
                }

                
                hoaDon.DaThanhToan = true;
                
            }
           

            _context.SaveChanges();

            return RedirectToAction("DonHang", "HoaDon");
        }

    }
}
