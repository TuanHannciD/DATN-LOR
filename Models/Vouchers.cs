using AuthDemo.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuthDemo.Models
{
    public class Vouchers : ThongTinChung
    {
        [Key]
        public Guid VoucherID { get; set; } 
        public string MaVoucherCode { get; set; }           // Mã voucher
        public string MoTa { get; set; }           // Mô tả
        public LoaiGiam LoaiGiam { get; set; }       // "PERCENT" hoặc "AMOUNT"
        public decimal GiaTri { get; set; }        // % hoặc số tiền giảm
        public decimal? GiaTriToiDa { get; set; }  // Giới hạn khi giảm %
        public decimal? DonHangToiThieu { get; set; } // Đơn tối thiểu
        public int? SoLanSuDung { get; set; }      // Giới hạn tổng số lần
        public int? SoLanSuDungMoiUser { get; set; } // Giới hạn mỗi user
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public bool TrangThai { get; set; } = true;             

        // Navigation
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
