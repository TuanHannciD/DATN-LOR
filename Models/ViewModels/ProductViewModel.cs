using AuthDemo.Models;

namespace AuthDemo.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid ShoeID { get; set; }
        public string TenGiay { get; set; }
        public string AnhDaiDien { get; set; }
        public decimal GiaThapNhat { get; set; }
        public Guid CategoryID { get; set; }        
        public Guid SizeID { get; set; }       
        public Guid ColorID { get; set; }         
        public Guid MaterialID { get; set; } 
        public Guid BrandID { get; set; } 
        public int SoLuong { get; set; }
    }
}
