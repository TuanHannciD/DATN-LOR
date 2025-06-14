using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class Gio_HangConfiguration : IEntityTypeConfiguration<Gio_Hang>
    {
        public void Configure(EntityTypeBuilder<Gio_Hang> builder)
        {
            builder.HasKey(gh => gh.ID_Gio_Hang);

            builder.HasOne(gh => gh.User_KhachHang)
                .WithMany()
                .HasForeignKey(gh => gh.ID_User)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed dữ liệu mẫu (đảm bảo ID_User từ User_KhachHang tồn tại)
            builder.HasData(
                new Gio_Hang { ID_Gio_Hang = Guid.NewGuid(), ID_User = new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") } // ID_User từ User_KhachHang đã seed
            );
        }
    }
} 
 