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
            builder.HasKey(kh => new { kh.Email, kh.HoTen });

            builder.HasOne(kh => kh.Admin)
                .WithMany()
                .HasForeignKey(kh => kh.HoTenAdmin)
                .OnDelete(DeleteBehavior.SetNull); // Có thể SetNull nếu Admin bị xóa mà KhachHang vẫn muốn giữ

            builder.HasOne(kh => kh.User)
                .WithMany()
                .HasForeignKey(kh => kh.UserName)
                .OnDelete(DeleteBehavior.Restrict); // Đã cấu hình Restrict trước đó

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_HoTen", "HoTen LIKE N'%[^a-zA-Z]% '"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_Email", "Email LIKE N'%_@gmail.com'"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_NgaySinh", "NgaySinh <= GETDATE()"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_GioiTinh", "GioiTinh IN (N'Nam', N'Nữ', N'Khác')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_DiemTichLuy", "DiemTichLuy >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhachHang_LoaiKhachHang", "LoaiKhachHang IN (N'VIP', N'Thường', N'Mới')"));

            // Seed khách hàng mẫu
            builder.HasData(new KhachHang
            {
                HoTen = "Tran Thi B",
                Email = "khachhangB@gmail.com",
                Sdt = "0901234567",
                DiaChi = "TP. Hồ Chí Minh",
                NgaySinh = new DateTime(1992, 8, 15),
                GioiTinh = "Nữ",
                TrangThai = "Hoạt động",
                NgayDangKy = DateTime.Now,
                DiemTichLuy = 100,
                LoaiKhachHang = "Thường",
                HoTenAdmin = "Admin Demo", // Đảm bảo Admin này đã có trong DB
                UserName = "testuser" // Đảm bảo User này đã có trong DB
            });
        }
    }
} 