using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPham_MuaConfiguration : IEntityTypeConfiguration<SanPham_Mua>
    {
        public void Configure(EntityTypeBuilder<SanPham_Mua> builder)
        {
            builder.HasKey(spm => spm.ID_SP_Mua);

            builder.HasOne(spm => spm.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(spm => spm.ID_Spct)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(spm => spm.KhuyenMai)
                .WithMany()
                .HasForeignKey(spm => spm.Ma_Km)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(spm => spm.User)
                .WithMany()
                .HasForeignKey(spm => spm.UserName)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_Spct, Ma_Km, UserName tồn tại)
            builder.HasData(
                new SanPham_Mua
                {
                    ID_SP_Mua = Guid.NewGuid(),
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), // ID_Spct đã seed
                    UserName = "testuser", // UserName đã seed
                    SoLuong = 1,
                    Gia = 150000,
                    Ma_Km = new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f") // Ma_Km đã seed
                }
            );
        }
    }
} 