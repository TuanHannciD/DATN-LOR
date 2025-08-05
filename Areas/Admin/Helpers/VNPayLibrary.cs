namespace AuthDemo.Areas.Admin.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Security.Cryptography;
    using System.Text;

    public class VNPayLibrary
    {
        private SortedList<string, string> requestData = new(new VNPayCompare());

        public void AddRequestData(string key, string value)
        {
            requestData[key] = value;
        }

        public string CreateRequestUrl(string baseUrl, string hashSecret)
        {
            var query = new StringBuilder();
            foreach (var kv in requestData)
            {
                query.AppendFormat("{0}={1}&", WebUtility.UrlEncode(kv.Key), WebUtility.UrlEncode(kv.Value));
            }

            string rawData = query.ToString().TrimEnd('&');
            string secureHash = ComputeHashHmacSHA512(hashSecret, rawData);
            return $"{baseUrl}?{rawData}&vnp_SecureHash={secureHash}";
        }

        private string ComputeHashHmacSHA512(string key, string input)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
    public class VNPayCompare : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            return string.Compare(x, y, StringComparison.Ordinal);
        }
    }

}