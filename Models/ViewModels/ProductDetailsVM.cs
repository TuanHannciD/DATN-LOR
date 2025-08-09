using AuthDemo.Models;

namespace AuthDemo.Models.ViewModels
{
    public class ProductDetailsVM
    {
        public Giay giay { get; set; }
        public ChiTietGiay ctgiay   { get; set; }
        public List<MauSac> MauSacOptions { get; set; }
        public List<string> DanhSachAnh { get; set; }
        public List<AnhGiay> anhgiay { get; set; }
        public List<KichThuoc> KichCoOptions { get; set; }
        public List<ThuongHieu> ThuongHieuOptions { get; set; }

        public List<SanPhamLqVM> SanPhamLienQuan { get; set; }
    }
}
