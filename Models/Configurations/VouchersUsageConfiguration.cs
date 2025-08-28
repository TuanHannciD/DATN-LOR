using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Models.Configurations
{
    public class VoucherUsageConfiguration : IEntityTypeConfiguration<VouchersUsage>
    {
        public void Configure(EntityTypeBuilder<VouchersUsage> builder)
        {
            builder.HasKey(vu => vu.VoucherUsageID);

            // Quan hệ với Voucher
            builder.HasOne(vu => vu.Vouchers)
                   .WithMany()
                   .HasForeignKey(vu => vu.VoucherID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với User
            builder.HasOne(vu => vu.NguoiDung)
                   .WithMany()
                   .HasForeignKey(vu => vu.UserID)
                   .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ với Hóa đơn
            builder.HasOne(vu => vu.HoaDon)
                   .WithMany()
                   .HasForeignKey(vu => vu.BillID)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
