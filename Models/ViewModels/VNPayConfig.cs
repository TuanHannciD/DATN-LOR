
using System.Text.Json.Serialization;

namespace AuthDemo.Models.ViewModels
{
    public class VNPayConfig
{
    public string BaseUrl { get; set; }
    public string TmnCode { get; set; }
    public string HashSecret { get; set; }
    public string ReturnUrl { get; set; }
    public string Version { get; set; } = "2.1.0";
    public string Command { get; set; } = "pay";
    public string CurrCode { get; set; } = "VND";
    public string Locale { get; set; } = "vn";
}


    public class VnpayOrderRequest
    {
        [JsonPropertyName("tongTien")]
        public decimal TongTien { get; set; }
        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }
        [JsonPropertyName("orderInfo")]
        public string OrderInfo { get; set; }
        public string BankCode { get; set; } = "VNBank";
        [JsonPropertyName("returnUrl")]
        public string ReturnURL { get; set; }
    }
}
