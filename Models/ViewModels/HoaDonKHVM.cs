namespace AuthDemo.Models.ViewModels
{
    public class HoaDonKHVM
    {
        public Guid BillID { get; set; }
        public string HoTen { get; set; }
        public string SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string TrangThai { get; set; }
        public DateTime ?NgayTao { get; set; }
        public decimal TongTien { get; set; }

        public string PhuongThuc { get; set; }
        public List<ChiTietHoaDonKHVM> ChiTiet { get; set; }

        public List<HoaDon> HoaDon { get; set; }

    }
}
