using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Services
{
    public class GhnService
    {
        private readonly HttpClient _http;
        private const string BASE_URL = "https://dev-online-gateway.ghn.vn/shiip/public-api";

        // Thay bằng thông tin của bạn
       

        public GhnService(HttpClient http)
        {
            _http = http;
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Token", "310e81e2-7e06-11f0-bdaf-ae7fa045a771");
            _http.DefaultRequestHeaders.Add("ShopId", "197311");
        }

        public async Task<string> GetProvinces()
        {
            var res = await _http.GetAsync($"{BASE_URL}/master-data/province");
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> GetDistricts(int provinceId)
        {
            var content = new StringContent($"{{\"province_id\":{provinceId}}}", System.Text.Encoding.UTF8, "application/json");
            var res = await _http.PostAsync($"{BASE_URL}/master-data/district", content);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> GetWards(int districtId)
        {
            var content = new StringContent($"{{\"district_id\":{districtId}}}", System.Text.Encoding.UTF8, "application/json");
            var res = await _http.PostAsync($"{BASE_URL}/master-data/ward", content);
            res.EnsureSuccessStatusCode();
            return await res.Content.ReadAsStringAsync();
        }
        public async Task<int?> GetServiceId(int shopId, int fromDistrict, int toDistrict, int serviceTypeId = 2)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://dev-online-gateway.ghn.vn/shiip/public-api/");
            client.DefaultRequestHeaders.Add("Token", "310e81e2-7e06-11f0-bdaf-ae7fa045a771");

            var data = new
            {
                shop_id = shopId,
                from_district = fromDistrict,
                to_district = toDistrict
            };

            var response = await client.PostAsJsonAsync("v2/shipping-order/available-services", data);
            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(result);

            var services = doc.RootElement.GetProperty("data").EnumerateArray();

            foreach (var service in services)
            {
                int serviceId = service.GetProperty("service_id").GetInt32();
                int typeId = service.GetProperty("service_type_id").GetInt32();
                if (typeId == serviceTypeId) return serviceId;
            }

            return null;
        }
        public async Task<string> TinhPhi(int districtId, string wardCode, int weight = 5000)
         {
            int serviceTypeId = 2;
            var serviceId = await GetServiceId(shopId: 197311, fromDistrict: 3440, toDistrict: districtId, serviceTypeId);
            var data = new
            {
                from_district_id = 3440,
                service_id = serviceId,
                to_district_id = districtId,       // int
                to_ward_code = wardCode.ToString(),// string
                height = 20,
                length = 20,
                weight = weight,                   // int
                width = 20

            };

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var res = await _http.PostAsync($"{BASE_URL}/v2/shipping-order/fee", content);
            var resContent = await res.Content.ReadAsStringAsync();
            if (!res.IsSuccessStatusCode)
            {
                Console.WriteLine(resContent); // sẽ in ra lý do 400
            }
            return resContent;

        }
    }
}