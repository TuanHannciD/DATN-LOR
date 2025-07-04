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
                new AnhSp { ID_AnhSp = new Guid("11111111-1111-1111-1111-111111111111"), FileAnh = "ao_thun_do_s_1.jpg", ID_Spct = new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee") },
                new AnhSp { ID_AnhSp = new Guid("22222222-2222-2222-2222-222222222222"), FileAnh = "ao_thun_do_s_2.jpg", ID_Spct = new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee") },
                new AnhSp { ID_AnhSp = new Guid("33333333-3333-3333-3333-333333333333"), FileAnh = "quan_jean_xanh_m_1.jpg", ID_Spct = new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff") }
            );
        }
    }
} 