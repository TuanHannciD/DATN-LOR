using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class HoTroKhachHangConfiguration : IEntityTypeConfiguration<HoTroKhachHang>
    {
        public void Configure(EntityTypeBuilder<HoTroKhachHang> builder)
        {
            builder.HasKey(htkh => htkh.ID_HoTroKhachHang);

            builder.HasOne(htkh => htkh.NhanVien)
                .WithMany()
                .HasForeignKey(htkh => htkh.MaNV)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(htkh => htkh.User_KhachHang)
                .WithMany()
                .HasForeignKey(htkh => htkh.ID_User)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo MaNV và ID_User từ User_KhachHang tồn tại)
            builder.HasData(
                new HoTroKhachHang
                {
                    ID_HoTroKhachHang = Guid.NewGuid(),
                    MaNV = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), // MaNV của NhanVien đã seed (giả định)
                    ID_User = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), // ID_User từ User_KhachHang đã seed
                    LoaiHT = "Hỗ trợ kỹ thuật",
                    PTLienLac = "Email"
                }
            );
        }
    }
} 