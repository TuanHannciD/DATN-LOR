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
                new ChatLieu { ID_ChatLieu = Guid.NewGuid(), ChatLieuName = "Cotton" },
                new ChatLieu { ID_ChatLieu = Guid.NewGuid(), ChatLieuName = "Len" },
                new ChatLieu { ID_ChatLieu = Guid.NewGuid(), ChatLieuName = "Lụa" }
            );
        }
    }
} 