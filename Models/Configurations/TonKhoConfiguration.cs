using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class TonKhoConfiguration : IEntityTypeConfiguration<TonKho>
    {
        public void Configure(EntityTypeBuilder<TonKho> builder)
        {
            builder.HasKey(tk => tk.ID_TonKho);

            builder.HasOne(tk => tk.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(tk => tk.ID_Spct)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(tk => tk.NgayCapNhap).HasDefaultValueSql("GETDATE()");

            // Seed dữ liệu mẫu (đảm bảo ID_Spct tồn tại)
            builder.HasData(
                new TonKho
                {
                    ID_TonKho = new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"),
                    ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"),
                    SoLuongTonKho = 50,
                    NgayCapNhap = new DateTime(2025, 6, 15)
                }
            );
        }
    }
} 