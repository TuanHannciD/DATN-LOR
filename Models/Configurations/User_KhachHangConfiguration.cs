using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class User_KhachHangConfiguration : IEntityTypeConfiguration<User_KhachHang>
    {
        public void Configure(EntityTypeBuilder<User_KhachHang> builder)
        {
            builder.HasKey(uk => uk.ID_User);

            builder.HasOne(uk => uk.KhachHang)
                .WithMany()
                .HasForeignKey(uk => new { uk.Email, uk.HoTen })
                .HasPrincipalKey(kh => new { kh.Email, kh.HoTen })
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(uk => uk.User)
                .WithMany()
                .HasForeignKey(uk => uk.UserName)
                .OnDelete(DeleteBehavior.Restrict); // Đã cấu hình Restrict trước đó

            // Seed User_KhachHang mẫu
            builder.HasData(
                new User_KhachHang
                {
                    ID_User = Guid.NewGuid(),
                    HoTen = "Tran Thi B",
                    UserName = "testuser", // Đảm bảo User này đã có trong DB
                    Email = "khachhangB@gmail.com" // Đảm bảo KhachHang này đã có trong DB
                }
            );
        }
    }
} 