using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPhamYeuThichConfiguration : IEntityTypeConfiguration<SanPhamYeuThich>
    {
        public void Configure(EntityTypeBuilder<SanPhamYeuThich> builder)
        {
            builder.HasKey(spyt => spyt.ID_Spyt);

            builder.HasOne(spyt => spyt.User_KhachHang)
                .WithMany()
                .HasForeignKey(spyt => spyt.ID_User)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_User từ User_KhachHang tồn tại)
            builder.HasData(
                new SanPhamYeuThich { ID_Spyt = Guid.NewGuid(), ID_User = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") } // ID_User từ User_KhachHang đã seed
            );
        }
    }
} 