using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace AuthDemo.Helpers
{
    public static class Utilities
    {
        // Hàm mã hóa mật khẩu sử dụng BCrypt
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }


        // Hàm kiểm tra mật khẩu
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }
        public static string GenerateDefaultPassword(string fullName, string phoneNumber)
        {
            string nameNormalized = RemoveDiacritics(fullName).Replace(" ", "").ToLower();
            return nameNormalized + phoneNumber;
        }

        public static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

    }
}
