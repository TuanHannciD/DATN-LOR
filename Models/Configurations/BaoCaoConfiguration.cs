using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class BaoCaoConfiguration : IEntityTypeConfiguration<BaoCao>
    {
        public void Configure(EntityTypeBuilder<BaoCao> builder)
        {
            builder.HasKey(bc => bc.ID_BaoCao);

            builder.HasOne(bc => bc.Admin)
                .WithMany()
                .HasForeignKey(bc => bc.HoTenAdmin)
                .OnDelete(DeleteBehavior.Cascade);

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_LoaiBaoCao", "LoaiBaoCao IN (N'DoanhSo', N'HangTon', N'DonHang', N'KhachHang')"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_DoanhThu", "DoanhThu >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongHangBanRa", "SoLuongHangBanRa >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongHangTon", "SoLuongHangTon >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_TongSoDonHang", "TongSoDonHang >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangHoanThanh", "SoLuongDonHangHoanThanh >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangDangXuLy", "SoLuongDonHangDangXuLy >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangBiHuy", "SoLuongDonHangBiHuy >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_TongSoKhachHang", "TongSoKhachHang >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoKhachHangMoi", "SoKhachHangMoi >= 0"));
            builder.ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoKhachHangTroLai", "SoKhachHangTroLai >= 0"));

            // Seed dữ liệu mẫu (đảm bảo HoTenAdmin tồn tại)
            builder.HasData(
                new BaoCao
                {
                    ID_BaoCao = Guid.NewGuid(),
                    NgayBaoCao = DateTime.Now,
                    LoaiBaoCao = "DoanhSo",
                    DoanhThu = 10000000.0m,
                    SoLuongHangBanRa = 500,
                    SoLuongHangTon = 2000,
                    TongSoDonHang = 100,
                    SoLuongDonHangHoanThanh = 80,
                    SoLuongDonHangDangXuLy = 15,
                    SoLuongDonHangBiHuy = 5,
                    TongSoKhachHang = 200,
                    SoKhachHangMoi = 20,
                    SoKhachHangTroLai = 180,
                    NgayTao = DateTime.Now,
                    NgayCapNhap = DateTime.Now,
                    HoTenAdmin = "Admin Demo" // Admin đã seed
                }
            );
        }
    }
} 