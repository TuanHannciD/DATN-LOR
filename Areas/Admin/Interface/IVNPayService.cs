using AuthDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IVNPayService
    {
        string CreatePaymentUrl(VnpayOrderRequest model, HttpContext httpContext);
    }

}