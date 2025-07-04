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
                new Size { ID_Size = new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), SizeName = "S" },
                new Size { ID_Size = new Guid("eeddccbb-aa99-8877-6655-443322110000"), SizeName = "M" }
            );
        }
    }
} 