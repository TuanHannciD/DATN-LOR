using AuthDemo.Areas.Admin.Helpers;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Data;
using AuthDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Services
{
    public class VNPayService : IVNPayService
    {
        private readonly VNPayConfig _vnpConfig;
        private readonly ILogger<VNPayService> _logger;

        public VNPayService(IOptions<VNPayConfig> options, ILogger<VNPayService> logger)
        {
            _vnpConfig = options.Value;
            _logger = logger;
        }

        public string CreatePaymentUrl(VnpayOrderRequest model, HttpContext context)
        {
            try
            {
                var vnpay = new VNPayLibrary();
                var now = DateTime.Now;
                var txnRef = now.Ticks.ToString();

                vnpay.AddRequestData("vnp_Version", _vnpConfig.Version);
                vnpay.AddRequestData("vnp_Command", _vnpConfig.Command);
                vnpay.AddRequestData("vnp_TmnCode", _vnpConfig.TmnCode);
                vnpay.AddRequestData("vnp_Amount", ((int)(model.TongTien * 100)).ToString());
                vnpay.AddRequestData("vnp_CreateDate", now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", _vnpConfig.CurrCode);
                string ipAddress = context.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "127.0.0.1";
                // Nếu đang chạy local (localhost, dev), ép về "127.0.0.1"
                if (ipAddress == "::1" || ipAddress == "0.0.0.1")
                {
                    ipAddress = "127.0.0.1";
                }
                vnpay.AddRequestData("vnp_IpAddr", ipAddress);
                vnpay.AddRequestData("vnp_Locale", _vnpConfig.Locale);
                vnpay.AddRequestData("vnp_OrderInfo", model.OrderInfo ?? "Thanh toán đơn hàng"); 

                vnpay.AddRequestData("vnp_OrderType", "other");
                vnpay.AddRequestData("vnp_ReturnUrl",  _vnpConfig.ReturnUrl);
                vnpay.AddRequestData("vnp_TxnRef", txnRef);

                string paymentUrl = vnpay.CreateRequestUrl(_vnpConfig.BaseUrl, _vnpConfig.HashSecret);

                _logger.LogInformation("Created VNPay payment URL: {url}", paymentUrl);

                return paymentUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo URL thanh toán VNPay");
                throw new ApplicationException("Không thể tạo URL thanh toán", ex);
            }
        }
    }

}