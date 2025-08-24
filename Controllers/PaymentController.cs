using AuthDemo.Models.VnPay;
using AuthDemo.Services.VnPay;
using Microsoft.AspNetCore.Mvc;

namespace DuAnTotNghiep2024.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IVnPayService _vnPayService;
        public PaymentController(IVnPayService vnPayService)
        {

            _vnPayService = vnPayService;
        }
        [HttpGet]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        

    }
}
