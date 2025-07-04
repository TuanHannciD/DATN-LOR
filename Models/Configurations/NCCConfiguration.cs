using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class NCCConfiguration : IEntityTypeConfiguration<NCC>
    {
        public void Configure(EntityTypeBuilder<NCC> builder)
        {
            builder.HasKey(ncc => ncc.Ma_NCC);

            builder.HasOne(ncc => ncc.Admin)
                .WithMany()
                .HasForeignKey(ncc => ncc.HoTenAdmin)
                .OnDelete(DeleteBehavior.Cascade);

            // Các ràng buộc CHECK
            builder.ToTable(tb => tb.HasCheckConstraint("CK_NCC_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động')"));

            // Seed dữ liệu mẫu (đảm bảo HoTenAdmin tồn tại)
            builder.HasData(
                new NCC
                {
                    Ma_NCC = new Guid("22222222-3333-4444-5555-666666666666"),
                    NameNCC = "Nhà cung cấp A",
                    NameNLH = "Nguyen Van Test",
                    SDT = "0123456789",
                    Email = "nccA@example.com",
                    DiaChi = "Hà Nội",
                    ThanhPho = "Hà Nội",
                    QuocGia = "Việt Nam",
                    MoTa = "Chuyên cung cấp vải cotton",
                    TrangThai = "Hoạt động",
                    CreatedDate = new DateTime(2025, 6, 10),
                    UpdatedDate = new DateTime(2025, 6, 15),
                    HoTenAdmin = "Admin Demo"
                }
            );

            builder.Property(ncc => ncc.CreatedDate).HasDefaultValueSql("GETDATE()");
            builder.Property(ncc => ncc.UpdatedDate).HasDefaultValueSql("GETDATE()");
        }
    }
} 