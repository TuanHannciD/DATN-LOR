using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;

namespace AuthDemo.Models.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.HoTenAdmin);
            builder.HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed admin mẫu
            builder.HasData(new Admin
            {
                HoTenAdmin = "Admin Demo",
                AnhDaiDien = "avatar.png",
                NgaySinh = new DateTime(1990, 1, 1),
                Email = "admin@gmail.com",
                Sdt = "0123456789",
                DiaChi = "Hà Nội",
                UserName = "testuser"
            });
        }
    }
} 