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
        public IActionResult VNPayReturn()
        {
            var queryParams = Request.Query.ToDictionary(q => q.Key, q => q.Value.ToString(), StringComparer.OrdinalIgnoreCase);
            var result = _vnpayService.ProcessPaymentReturn(queryParams);

            // Chuyển hướng về trang gốc kèm query params kết quả thanh toán
            var redirectUrl = $"/Admin/BanHangTaiQuay?vnp_ResponseCode={result.ResponseCode}&vnp_TxnRef={result.OrderId}&vnp_Amount={result.Amount}";
            
            return Redirect(redirectUrl);
        }
    }
}
