using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class Don_Hang_Thanh_ToanConfiguration : IEntityTypeConfiguration<Don_Hang_Thanh_Toan>
    {
        public void Configure(EntityTypeBuilder<Don_Hang_Thanh_Toan> builder)
        {
            builder.HasKey(dhtt => dhtt.ID_Don_Hang_Thanh_Toan);

            builder.HasOne(dhtt => dhtt.ThanhToan)
                .WithMany()
                .HasForeignKey(dhtt => dhtt.ID_ThanhToan)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(dhtt => dhtt.DonHang)
                .WithMany()
                .HasForeignKey(dhtt => dhtt.ID_Don_Hang)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_ThanhToan và ID_Don_Hang tồn tại)
            builder.HasData(
                new Don_Hang_Thanh_Toan
                {
                    ID_Don_Hang_Thanh_Toan = new Guid("11111111-aaaa-bbbb-cccc-555555555555"),
                    ID_ThanhToan = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"),
                    ID_Don_Hang = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"),
                    Status = "Hoàn thành",
                    NgayTT = new DateTime(2025, 6, 15),
                    KieuTT = "Tiền mặt"
                }
            );
        }
    }
} 