using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IVNPayService _vnpayService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IVNPayService vnpayService, ILogger<PaymentController> logger)
        {
            _vnpayService = vnpayService;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreatePayment([FromBody] VnpayOrderRequest model)
        {
            try
            {
                if (model.TongTien <= 0)
                    return BadRequest(new { message = "Số tiền không hợp lệ" });

                var paymentUrl = _vnpayService.CreatePaymentUrl(model, HttpContext);
                return Ok(new { paymentUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo thanh toán");
                return StatusCode(500, "Lỗi server khi tạo thanh toán VNPay.");
            }
        }



        [HttpGet]
        public IActionResult VnPayReturn()
        {
            // TODO: Xử lý phản hồi từ VNPay sau khi thanh toán thành công/thất bại
            return View();
        }
    }
}
