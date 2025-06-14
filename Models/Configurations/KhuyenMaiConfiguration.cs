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
                    Ma_Km = Guid.NewGuid(),
                    TenKm = "Giảm giá mùa hè",
                    MoTa = "Giảm 10% cho tất cả sản phẩm",
                    LoaiKm = "Phần trăm",
                    GiaTriKm = 10.0m,
                    NgayBd = new DateTime(2024, 6, 1),
                    NgayKt = new DateTime(2024, 8, 31),
                    SoLuong = 1000,
                    SoLuong1Ng = 100,
                    HoTenAdmin = "Admin Demo" // Admin đã seed
                },
                new KhuyenMai
                {
                    Ma_Km = Guid.NewGuid(),
                    TenKm = "Miễn phí vận chuyển",
                    MoTa = "Miễn phí giao hàng cho đơn hàng trên 500k",
                    LoaiKm = "Miễn phí giao hàng",
                    GiaTriKm = 0.0m,
                    NgayBd = new DateTime(2024, 7, 1),
                    NgayKt = new DateTime(2024, 7, 31),
                    SoLuong = 500,
                    SoLuong1Ng = 50,
                    HoTenAdmin = "Admin Demo" // Admin đã seed
                }
            );
        }
    }
} 