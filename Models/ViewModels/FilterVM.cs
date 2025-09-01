namespace AuthDemo.Models.ViewModels
{
    public class HoaDonFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TrangThai { get; set; } = "";
        public string HinhThuc { get; set; } = "";
        public string Phone { get; set; } = "";
        public string IdFilter { get; set; } = "";
        public string NameFilter { get; set; } = "";
        public bool? TrangThaiTT { get; set; }
        public string NameCreateFilter { get; set; } = "";
        public string TongTienFilter { get; set; } = "";
    }
    public class ChiTietSanPhamVM
    {
        
    }
}
