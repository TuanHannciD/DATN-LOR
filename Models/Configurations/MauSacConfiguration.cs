using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class MauSacConfiguration : IEntityTypeConfiguration<MauSac>
    {
        public void Configure(EntityTypeBuilder<MauSac> builder)
        {
            builder.HasKey(ms => ms.ID_MauSac);

            // Seed dữ liệu mẫu
            builder.HasData(
                new MauSac
                {
                    ID_MauSac = new Guid("b1a7e8c2-3f4d-4e6a-9b2c-1a2b3c4d5e6f"),
                    MauSacName = "Đỏ"
                },
                new MauSac { ID_MauSac = new Guid("22334455-6677-8899-aabb-ccddeeff2233"), MauSacName = "Xanh" },
                new MauSac { ID_MauSac = new Guid("11111111-2222-3333-4444-555555555555"), MauSacName = "Đen" },
                new MauSac { ID_MauSac = new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"), MauSacName = "Trắng" },
                new MauSac { ID_MauSac = new Guid("11223344-5566-7788-99aa-bbccddeeff11"), MauSacName = "Vàng" }
            );
        }
    }
} 