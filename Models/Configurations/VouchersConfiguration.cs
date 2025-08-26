using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AuthDemo.Models.Configurations
{
    public class VouchersConfiguration : IEntityTypeConfiguration<Vouchers>
    {
        public void Configure(EntityTypeBuilder<Vouchers> builder)
        {
            builder.HasKey(v => v.VoucherID);

            builder.Property(v => v.MaVoucherCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(v => v.MoTa)
                .HasMaxLength(250);

            builder.Property(v => v.LoaiGiam)
                .IsRequired()
                .HasMaxLength(50);

            //builder.Property(v => v.GiaTri)
            //    .HasColumnType("decimal(18,2)");

            //builder.Property(v => v.GiaTriToiDa)
            //    .HasColumnType("decimal(18,2)");

            //builder.Property(v => v.DonHangToiThieu)
            //    .HasColumnType("decimal(18,2)");

            // 1 Voucher có thể gắn với nhiều Hóa đơn
            builder.HasMany(v => v.HoaDons)
                   .WithOne(h => h.Vouchers)
                   .HasForeignKey(h => h.VoucherID)
                   .OnDelete(DeleteBehavior.SetNull); // Nếu xóa voucher thì bill vẫn giữ
        }
    }
}
