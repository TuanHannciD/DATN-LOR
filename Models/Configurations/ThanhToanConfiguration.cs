using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class ThanhToanConfiguration : IEntityTypeConfiguration<ThanhToan>
    {
        public void Configure(EntityTypeBuilder<ThanhToan> builder)
        {
            builder.HasKey(tt => tt.ID_ThanhToan);

            builder.HasOne(tt => tt.HoaDon)
                .WithMany()
                .HasForeignKey(tt => tt.ID_HoaDon)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_HoaDon tồn tại)
            builder.HasData(
                new ThanhToan
                {
                    ID_ThanhToan = Guid.NewGuid(),
                    ID_HoaDon = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), // ID_HoaDon đã seed (Giả định một GUID)
                    PhuongThucThanhToan = "Chuyển khoản",
                    Status = "Đã thanh toán",
                    SoTienThanhToan = 150000,
                    DiaChi = "Hà Nội",
                    SDT = "0912345678",
                    HoTen = "Tran Thi B",
                    GhiChu = "Thanh toán đơn hàng #1"
                }
            );
        }
    }
} 