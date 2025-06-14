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
                new MauSac { ID_MauSac = Guid.NewGuid(), MauSacName = "Đỏ" },
                new MauSac { ID_MauSac = Guid.NewGuid(), MauSacName = "Xanh" },
                new MauSac { ID_MauSac = Guid.NewGuid(), MauSacName = "Đen" },
                new MauSac { ID_MauSac = Guid.NewGuid(), MauSacName = "Trắng" }
            );
        }
    }
} 