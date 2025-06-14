using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPhamYeuThichChiTietConfiguration : IEntityTypeConfiguration<SanPhamYeuThichChiTiet>
    {
        public void Configure(EntityTypeBuilder<SanPhamYeuThichChiTiet> builder)
        {
            builder.HasKey(spytct => spytct.ID_Spyt_Chi_Tiet);

            builder.HasOne(spytct => spytct.SanPhamYeuThich)
                .WithMany()
                .HasForeignKey(spytct => spytct.ID_Spyt)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(spytct => spytct.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(spytct => spytct.ID_Spct)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_Spyt và ID_Spct tồn tại)
            builder.HasData(
                new SanPhamYeuThichChiTiet
                {
                    ID_Spyt_Chi_Tiet = Guid.NewGuid(),
                    ID_Spyt = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), // ID_Spyt đã seed
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), // ID_Spct đã seed
                    Gia = 150000
                }
            );
        }
    }
} 