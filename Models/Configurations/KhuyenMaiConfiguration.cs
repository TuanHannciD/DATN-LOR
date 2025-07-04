using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class KhuyenMaiConfiguration : IEntityTypeConfiguration<KhuyenMai>
    {
        public void Configure(EntityTypeBuilder<KhuyenMai> builder)
        {
            builder.HasKey(km => km.Ma_Km);

            builder.HasOne(km => km.Admin)
                .WithMany()
                .HasForeignKey(km => km.HoTenAdmin)
                .OnDelete(DeleteBehavior.Cascade);

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_GiaTriKm", "GiaTriKm > 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_LoaiKm", "LoaiKm IN (N'Phần trăm', N'Số tiền cố định', N'Miễn phí giao hàng')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_SoLuong", "SoLuong >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_SoLuong1Ng", "SoLuong1Ng >= 0"));

            // Seed dữ liệu mẫu (đảm bảo HoTenAdmin tồn tại)
            builder.HasData(
                new KhuyenMai
                {
                    Ma_Km = new Guid("dddddddd-eeee-ffff-1111-222222222222"),
                    TenKm = "Giảm giá mùa hè",
                    MoTa = "Giảm 10% cho tất cả sản phẩm",
                    LoaiKm = "Phần trăm",
                    GiaTriKm = 10.0m,
                    NgayBd = new DateTime(2025, 6, 1),
                    NgayKt = new DateTime(2025, 8, 31),
                    SoLuong = 1000,
                    SoLuong1Ng = 100,
                    HoTenAdmin = "Admin Demo"
                },
                new KhuyenMai
                {
                    Ma_Km = new Guid("eeeeeeee-ffff-1111-2222-333333333333"),
                    TenKm = "Miễn phí vận chuyển",
                    MoTa = "Miễn phí giao hàng cho đơn hàng trên 500k",
                    LoaiKm = "Miễn phí giao hàng",
                    GiaTriKm = 12,
                    NgayBd = new DateTime(2025, 7, 1),
                    NgayKt = new DateTime(2025, 7, 31),
                    SoLuong = 500,
                    SoLuong1Ng = 50,
                    HoTenAdmin = "Admin Demo"
                },
                new KhuyenMai
                {
                    Ma_Km = new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f"),
                    TenKm = "Tặng voucher đặc biệt",
                    MoTa = "Voucher giảm 50k cho đơn hàng trên 300k",
                    LoaiKm = "Số tiền cố định",
                    GiaTriKm = 50000.0m,
                    NgayBd = new DateTime(2025, 7, 1),
                    NgayKt = new DateTime(2025, 7, 31),
                    SoLuong = 100,
                    SoLuong1Ng = 5,
                    HoTenAdmin = "Admin Demo"
                }
            );
        }
    }
} 