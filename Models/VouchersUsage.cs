namespace AuthDemo.Models
{
    public class VouchersUsage :ThongTinChung
    {
        public Guid VoucherUsageID { get; set; } = Guid.NewGuid();

        public Guid VoucherID { get; set; }
        public Guid UserID { get; set; }
        public Guid BillID { get; set; }   // Lưu đơn hàng nào đã dùng voucher này

        // Navigation
        public Vouchers Vouchers { get; set; }
        public NguoiDung NguoiDung { get; set; }
        public HoaDon HoaDon { get; set; }
    }
}
