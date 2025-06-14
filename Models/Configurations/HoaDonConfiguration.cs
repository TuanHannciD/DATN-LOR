using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class HoaDonConfiguration : IEntityTypeConfiguration<HoaDon>
    {
        public void Configure(EntityTypeBuilder<HoaDon> builder)
        {
            builder.HasKey(hd => hd.ID_HoaDon);

            builder.HasOne(hd => hd.User_KhachHang)
                .WithMany()
                .HasForeignKey(hd => hd.ID_User)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_User từ User_KhachHang tồn tại)
            builder.HasData(
                new HoaDon
                {
                    ID_HoaDon = Guid.NewGuid(),
                    ID_User = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") // ID_User từ User_KhachHang đã seed
                }
            );
        }
    }
} 