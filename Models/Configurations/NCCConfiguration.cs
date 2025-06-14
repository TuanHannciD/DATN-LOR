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
                    Ma_NCC = Guid.NewGuid(),
                    NameNCC = "Nhà cung cấp A",
                    NameNLH = "Nguyen Van Test",
                    SDT = "0123456789",
                    Email = "nccA@example.com",
                    DiaChi = "Hà Nội",
                    ThanhPho = "Hà Nội",
                    QuocGia = "Việt Nam",
                    MoTa = "Chuyên cung cấp vải cotton",
                    TrangThai = "Hoạt động",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    HoTenAdmin = "Admin Demo" // Admin đã seed
                }
            );
        }
    }
} 