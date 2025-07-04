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
                new HangSX { ID_Hang = new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"), HangSXName = "Nike" },
                new HangSX { ID_Hang = new Guid("f1e2d3c4-b5a6-9d8e-7c6b-5a4d3c2b1a0e"), HangSXName = "Adidas" }
            );
        }
    }
} 