using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class DonHangConfiguration : IEntityTypeConfiguration<DonHang>
    {
        public void Configure(EntityTypeBuilder<DonHang> builder)
        {
            builder.HasKey(dh => dh.ID_Don_Hang);

            builder.HasOne(dh => dh.NhanVien)
                .WithMany()
                .HasForeignKey(dh => dh.MaNV)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo MaNV từ NhanVien tồn tại)
            builder.HasData(
                new DonHang
                {
                    ID_Don_Hang = new Guid("55555555-5555-5555-5555-555555555555"),
                    MaNV = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c") // MaNV của NhanVien đã seed (giả định)
                },
                new DonHang
                {
                    ID_Don_Hang = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"),
                    MaNV = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c")
                }
            );
        }
    }
} 