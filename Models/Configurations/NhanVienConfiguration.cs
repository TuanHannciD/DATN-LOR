using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;

namespace AuthDemo.Models.Configurations
{
    public class NhanVienConfiguration : IEntityTypeConfiguration<NhanVien>
    {
        public void Configure(EntityTypeBuilder<NhanVien> builder)
        {
            builder.HasKey(nv => nv.MaNV);

            builder.HasOne(nv => nv.Admin)
                .WithMany()
                .HasForeignKey(nv => nv.HoTenAdmin)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(nv => nv.User)
                .WithMany()
                .HasForeignKey(nv => nv.UserName)
                .OnDelete(DeleteBehavior.Restrict); // Đã cấu hình Restrict trước đó

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NhanVien_NgaySinh", "NgaySinh <= GETDATE()"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NhanVien_GioiTinh", "GioiTinh IN (N'Nam', N'Nữ', N'Khác')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NhanVien_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NhanVien_LuongCoBan", "LuongCoBan > 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NhanVien_SoGioLamViec", "SoGioLamViec >= 0"));

            // Seed nhân viên mẫu
            builder.HasData(new NhanVien
            {
                MaNV = Guid.NewGuid(),
                HoTenNV = "Nguyen Van A",
                HoTenAdmin = "Admin Demo", // Đảm bảo Admin này đã có trong DB
                Sdt = "0987654321",
                Email = "nhanvienA@example.com",
                DiaChi = "Hà Nội",
                NgaySinh = new DateTime(1995, 5, 10),
                GioiTinh = "Nam",
                TrangThai = "Hoạt động",
                NgayVaoLam = new DateTime(2020, 1, 1),
                ChucVu = "Quản lý",
                LuongCoBan = 15000000,
                SoGioLamViec = 160,
                UserName = "testuser" // Đảm bảo User này đã có trong DB
            });
        }
    }
} 