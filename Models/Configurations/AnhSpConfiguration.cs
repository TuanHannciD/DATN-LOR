using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class AnhSpConfiguration : IEntityTypeConfiguration<AnhSp>
    {
        public void Configure(EntityTypeBuilder<AnhSp> builder)
        {
            builder.HasKey(asp => asp.ID_AnhSp);

            builder.HasOne(asp => asp.SanPhamChiTiet)
                .WithMany()
                .HasForeignKey(asp => asp.ID_Spct)
                .OnDelete(DeleteBehavior.SetNull); // SetNull nếu SanPhamChiTiet bị xóa

            // Seed dữ liệu mẫu (đảm bảo ID_Spct tồn tại)
            builder.HasData(
                new AnhSp { ID_AnhSp = Guid.NewGuid(), FileAnh = "ao_thun_do_s_1.jpg", ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b") }, // ID_Spct từ SanPhamChiTiet đã seed
                new AnhSp { ID_AnhSp = Guid.NewGuid(), FileAnh = "ao_thun_do_s_2.jpg", ID_Spct = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b") },
                new AnhSp { ID_AnhSp = Guid.NewGuid(), FileAnh = "quan_jean_xanh_m_1.jpg", ID_Spct = new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b") }
            );
        }
    }
} 