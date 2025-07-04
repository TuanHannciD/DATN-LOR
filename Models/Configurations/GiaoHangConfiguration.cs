using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class GiaoHangConfiguration : IEntityTypeConfiguration<GiaoHang>
    {
        public void Configure(EntityTypeBuilder<GiaoHang> builder)
        {
            builder.HasKey(gh => gh.ID_GiaoHang);

            builder.HasOne(gh => gh.ThanhToan)
                .WithMany()
                .HasForeignKey(gh => gh.ID_ThanhToan)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(gh => gh.DonHang)
                .WithMany()
                .HasForeignKey(gh => gh.ID_Don_Hang)
                .OnDelete(DeleteBehavior.SetNull); // Có thể SetNull nếu DonHang bị xóa

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_GiaoHang_TrangThaiGiaoHang", "TrangThaiGiaoHang IN (N'Chưa phân công', N'Đang giao', N'Đã giao', N'Đang xử lý', N'Đang chờ giao hàng', N'Gặp sự cố', N'Bị hủy', N'Giao lại')"));

            // Seed dữ liệu mẫu (đảm bảo ID_ThanhToan và ID_Don_Hang tồn tại)
            builder.HasData(
                new GiaoHang
                {
                    ID_GiaoHang = new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                    ID_ThanhToan = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"),
                    ID_Don_Hang = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"),
                    NgayPhanCongGiaoHang = new DateTime(2024, 6, 10),
                    ThoiGianDuKienGiaoHang = new DateTime(2024, 6, 15),
                    ThoiGianThucTeGiaoHang = null,
                    TrangThaiGiaoHang = "Đang xử lý",
                    NgayTao = new DateTime(2025, 6, 8),
                    NgayCapNhap = new DateTime(2025, 6, 10)
                }
            );

            builder.Property(gh => gh.NgayPhanCongGiaoHang).HasDefaultValueSql("GETDATE()");
            builder.Property(gh => gh.NgayTao).HasDefaultValueSql("GETDATE()");
            builder.Property(gh => gh.NgayCapNhap).HasDefaultValueSql("GETDATE()");
        }
    }
} 