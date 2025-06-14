using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class Gio_Hang_Chi_TietConfiguration : IEntityTypeConfiguration<Gio_Hang_Chi_Tiet>
    {
        public void Configure(EntityTypeBuilder<Gio_Hang_Chi_Tiet> builder)
        {
            builder.HasKey(ghct => ghct.ID_Gio_Hang_Chi_Tiet);

            builder.HasOne(ghct => ghct.Gio_Hang)
                .WithMany()
                .HasForeignKey(ghct => ghct.ID_Gio_Hang)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ghct => ghct.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(ghct => ghct.ID_Spct)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ghct => ghct.KhuyenMai)
                .WithMany()
                .HasForeignKey(ghct => ghct.Ma_Km)
                .OnDelete(DeleteBehavior.SetNull); // Có thể SetNull nếu KhuyenMai bị xóa

            // Seed dữ liệu mẫu (đảm bảo ID_Gio_Hang, ID_Spct, Ma_Km tồn tại)
            builder.HasData(
                new Gio_Hang_Chi_Tiet
                {
                    ID_Gio_Hang_Chi_Tiet = Guid.NewGuid(),
                    ID_Gio_Hang = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), // ID_Gio_Hang đã seed
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), // ID_Spct đã seed
                    Ma_Km = new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f"), // Ma_Km đã seed
                    SoLuong = 1,
                    Gia = 150000
                },
                new Gio_Hang_Chi_Tiet
                {
                    ID_Gio_Hang_Chi_Tiet = Guid.NewGuid(),
                    ID_Gio_Hang = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), // ID_Gio_Hang đã seed
                    ID_Spct = new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), // ID_Spct đã seed
                    Ma_Km = null, // Không có khuyến mãi
                    SoLuong = 2,
                    Gia = 450000
                }
            );
        }
    }
} 