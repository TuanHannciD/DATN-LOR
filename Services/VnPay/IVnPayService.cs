using AuthDemo.Models.VnPay;
using AuthDemo.Models;

namespace AuthDemo.Services.VnPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);

    }
}
