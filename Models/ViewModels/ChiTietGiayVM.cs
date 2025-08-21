using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models.ViewModels
{
    public class ChiTietGiayVM
    {
        // ViewModel cho Index
        public class IndexVM
        {
            public Guid ShoeDetailID { get; set; }
            public string? TenGiay { get; set; }
            public string? TenKichThuoc { get; set; }
            public string? TenMau { get; set; }
            public string? TenChatLieu { get; set; }
            public string? TenThuongHieu { get; set; }
            public string? TenDanhMuc { get; set; }
            public int SoLuong { get; set; }
            public decimal Gia { get; set; }
        }

        // ViewModel cho Create
        public class CreateVM
        {
            public Guid ShoeID { get; set; }
            public Guid SizeIDs { get; set; }

            public Guid ColorIDs { set; get; }
            public Guid MaterialID { get; set; }
            public Guid BrandID { get; set; }
            public Guid CategoryID { get; set; }
            public int SoLuong { get; set; } = 0;
            public decimal Gia { get; set; } = 0;
            [Required]
            public List<ChiTietImageVM> ChiTietImages { get; set; } = new List<ChiTietImageVM>();
        }

        public class ChiTietImageVM
        {
            public Guid SizeID { get; set; }
            public Guid ColorID { get; set; }
            public List<string> Images { get; set; } = new List<string>();
        }
        // ViewModel cho Edit
        public class EditVM
        {
            public string? TenGiay { get; set; }
            public Guid ShoeDetailID { get; set; }
            public Guid ShoeID { get; set; }
            public Guid SizeID { get; set; }
            public Guid ColorID { get; set; }
            public Guid MaterialID { get; set; }
            public Guid BrandID { get; set; }
            public Guid CategoryID { get; set; }
            public int SoLuong { get; set; }
            public decimal Gia { get; set; }
            [Required]
            public string Image { get; set; }
        }

        public class QuickAddVM
        {
            public string? TenGiay { get; set; }
            public string? TenChatLieu { get; set; }
            public string? TenHang { get; set; }
            public string? TenDanhMuc { get; set; }
        }
    }
}
