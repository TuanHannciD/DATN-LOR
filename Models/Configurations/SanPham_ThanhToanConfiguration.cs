using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPham_ThanhToanConfiguration : IEntityTypeConfiguration<SanPham_ThanhToan>
    {
        public void Configure(EntityTypeBuilder<SanPham_ThanhToan> builder)
        {
            builder.HasKey(spt => spt.ID_Sp_ThanhToan);

            builder.HasOne(spt => spt.ThanhToan)
                .WithMany()
                .HasForeignKey(spt => spt.ID_ThanhToan)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(spt => spt.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(spt => spt.ID_Spct)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_ThanhToan và ID_Spct tồn tại)
            builder.HasData(
                new SanPham_ThanhToan
                {
                    ID_Sp_ThanhToan = Guid.NewGuid(),
                    ID_ThanhToan = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), // ID_ThanhToan đã seed (giả định)
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), // ID_Spct đã seed (giả định)
                    SoLuong = 1
                }
            );
        }
    }
} 