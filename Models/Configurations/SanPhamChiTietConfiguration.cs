using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPhamChiTietConfiguration : IEntityTypeConfiguration<SanPhamChiTiet>
    {
        public void Configure(EntityTypeBuilder<SanPhamChiTiet> builder)
        {
            builder.HasKey(spct => spct.ID_Spct);

            builder.HasOne(spct => spct.SanPham)
                .WithMany()
                .HasForeignKey(spct => spct.ID_Sp)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(spct => spct.ChatLieu)
                .WithMany()
                .HasForeignKey(spct => spct.ID_ChatLieu)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(spct => spct.HangSX)
                .WithMany()
                .HasForeignKey(spct => spct.ID_Hang)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(spct => spct.MauSac)
                .WithMany()
                .HasForeignKey(spct => spct.ID_MauSac)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(spct => spct.Size)
                .WithMany()
                .HasForeignKey(spct => spct.ID_Size)
                .OnDelete(DeleteBehavior.SetNull);

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_SanPhamChiTiet_SoLuongBan", "SoLuongBan >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_SanPhamChiTiet_Gia", "Gia > 0"));

            // Seed dữ liệu mẫu (đảm bảo ID_Sp, ID_ChatLieu, ID_Hang, ID_MauSac, ID_Size tồn tại nếu là GUID)
            builder.HasData(
                new SanPhamChiTiet
                {
                    ID_Spct = new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"),
                    ID_Sp = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"),
                    TenSp = "Áo thun cơ bản Đỏ S",
                    MoTa = "Áo thun cổ tròn, vải cotton mềm mại, màu đỏ, size S.",
                    SoLuongBan = 10,
                    Gia = 150000,
                    AnhDaiDien = "ao_thun_do_s.jpg",
                    DanhGia = "Tốt",
                    ID_ChatLieu = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                    ID_Hang = new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"),
                    ID_MauSac = new Guid("11223344-5566-7788-99aa-bbccddeeff11"),
                    ID_Size = new Guid("ffeeddcc-bbaa-9988-7766-554433221100")
                },
                new SanPhamChiTiet
                {
                    ID_Spct = new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"),
                    ID_Sp = new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"),
                    TenSp = "Quần jean slim fit Xanh M",
                    MoTa = "Quần jean dáng ôm, co giãn tốt, màu xanh, size M.",
                    SoLuongBan = 5,
                    Gia = 450000,
                    AnhDaiDien = "quan_jean_xanh_m.jpg",
                    DanhGia = "Tuyệt vời",
                    ID_ChatLieu = new Guid("8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d"),
                    ID_Hang = new Guid("f1e2d3c4-b5a6-9d8e-7c6b-5a4d3c2b1a0e"),
                    ID_MauSac = new Guid("22334455-6677-8899-aabb-ccddeeff2233"),
                    ID_Size = new Guid("eeddccbb-aa99-8877-6655-443322110000")
                },
                new SanPhamChiTiet
                {
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"),
                    ID_Sp = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"),
                    TenSp = "Áo thun đặc biệt",
                    MoTa = "Áo thun đặc biệt, vải cao cấp, màu đen, size M.",
                    SoLuongBan = 20,
                    Gia = 250000,
                    AnhDaiDien = "ao_thun_dac_biet.jpg",
                    DanhGia = "Xuất sắc",
                    ID_ChatLieu = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                    ID_Hang = new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"),
                    ID_MauSac = new Guid("11111111-2222-3333-4444-555555555555"),
                    ID_Size = new Guid("ffeeddcc-bbaa-9988-7766-554433221100")
                }
            );
        }
    }
} 