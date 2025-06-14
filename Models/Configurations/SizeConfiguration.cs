using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.HasKey(s => s.ID_Size);

            // Seed dữ liệu mẫu
            builder.HasData(
                new Size { ID_Size = Guid.NewGuid(), SizeName = "S" },
                new Size { ID_Size = Guid.NewGuid(), SizeName = "M" },
                new Size { ID_Size = Guid.NewGuid(), SizeName = "L" },
                new Size { ID_Size = Guid.NewGuid(), SizeName = "XL" }
            );
        }
    }
} 