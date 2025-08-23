namespace AuthDemo.Models.ViewModels
{
    public class ChiTietHoaDonKHVM
    {
        public string TenSanPham { get; set; }
        public string Size { get; set; }
        public string MauSac { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public ChiTietGiay ctgiay { get; set; }
    }
}
