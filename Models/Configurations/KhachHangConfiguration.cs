using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.HasKey(kh => kh.Email);
            builder.HasKey(kh => kh.HoTen);

            builder.HasOne(kh => kh.Admin)
                .WithMany()
                .HasForeignKey(kh => kh.HoTenAdmin)
                .OnDelete(DeleteBehavior.SetNull); // Có thể SetNull nếu Admin bị xóa mà KhachHang vẫn muốn giữ

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_HoTen", "HoTen LIKE N'%[^a-zA-Z]% '"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_Email", "Email LIKE N'%_@gmail.com'"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_NgaySinh", "NgaySinh <= GETDATE()"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_GioiTinh", "GioiTinh IN (N'Nam', N'Nữ', N'Khác')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_DiemTichLuy", "DiemTichLuy >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_LoaiKhachHang", "LoaiKhachHang IN (N'VIP', N'Thường', N'Mới')"));

            // Seed khách hàng mẫu
            builder.HasData(
                new KhachHang
                {
                    Email = "khachhangB@gmail.com",
                    HoTen = "Tran Thi B ",
                    Sdt = "0987654321",
                    DiaChi = "Hà Nội",
                    NgaySinh = new DateTime(2000, 1, 1),
                    GioiTinh = "Nữ",
                    TrangThai = "Hoạt động",
                    NgayDangKy = new DateTime(2025, 6, 28),
                    DiemTichLuy = 0,
                    LoaiKhachHang = "Thường",
                    HoTenAdmin = "Admin Demo",
                    UserName = "testuser"
                }
            );
        }
    }
} 