using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class HangSXConfiguration : IEntityTypeConfiguration<HangSX>
    {
        public void Configure(EntityTypeBuilder<HangSX> builder)
        {
            builder.HasKey(hsx => hsx.ID_Hang);

            // Seed dữ liệu mẫu
            builder.HasData(
                new HangSX { ID_Hang = Guid.NewGuid(), HangSXName = "Nike" },
                new HangSX { ID_Hang = Guid.NewGuid(), HangSXName = "Adidas" },
                new HangSX { ID_Hang = Guid.NewGuid(), HangSXName = "Puma" }
            );
        }
    }
} 