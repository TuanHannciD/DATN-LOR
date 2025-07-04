using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class ChatLieuConfiguration : IEntityTypeConfiguration<ChatLieu>
    {
        public void Configure(EntityTypeBuilder<ChatLieu> builder)
        {
            builder.HasKey(cl => cl.ID_ChatLieu);

            // Seed dữ liệu mẫu
            builder.HasData(
                new ChatLieu { ID_ChatLieu = new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), ChatLieuName = "Cotton" },
                new ChatLieu { ID_ChatLieu = new Guid("8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d"), ChatLieuName = "Len" }
            );
        }
    }
} 