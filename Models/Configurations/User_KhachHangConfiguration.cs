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
            // Khóa chính tổng hợp
            builder.HasKey(uk => new { uk.ID_User});

            // Quan hệ N-N: User - User_KhachHang
            builder.HasOne(uk => uk.User)
                .WithMany()
                .HasForeignKey(uk => uk.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan hệ N-N: KhachHang - User_KhachHang
            // Thiết lập quan hệ giữa User_KhachHang và KhachHang qua 2 trường HoTen và Email
            builder.HasOne(uk => uk.KhachHang)
                .WithMany()
                .HasForeignKey(uk => new { uk.HoTen, uk.Email })
                .HasPrincipalKey(kh => new { kh.HoTen, kh.Email })
                .OnDelete(DeleteBehavior.Cascade);

            // Seed User_KhachHang mẫu
            builder.HasData(
                new User_KhachHang
                {
                    ID_User = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"),
                    HoTen = "Tran Thi B ",
                    UserName = "testuser", // Đảm bảo User này đã có trong DB
                    Email = "khachhangB@gmail.com" // Đảm bảo KhachHang này đã có trong DB
                }
            );
        }
    }
} 