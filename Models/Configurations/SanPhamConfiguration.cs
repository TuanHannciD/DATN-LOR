using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AuthDemo.Models;
using System;

namespace AuthDemo.Models.Configurations
{
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.HasKey(sp => sp.ID_Sp);

            // Seed dữ liệu mẫu
            builder.HasData(
                new SanPham { ID_Sp = new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), Ten_Sp = "Áo thun cơ bản", SoLuongTong = 100, MoTa = "Áo thun cổ tròn, vải cotton mềm mại.", TrangThai = "Còn hàng" },
                new SanPham { ID_Sp = new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), Ten_Sp = "Quần jean slim fit", SoLuongTong = 50, MoTa = "Quần jean dáng ôm, co giãn tốt.", TrangThai = "Còn hàng" },
                new SanPham { ID_Sp = new Guid("33333333-4444-5555-6666-777777777777"), Ten_Sp = "Giày thể thao A", SoLuongTong = 75, MoTa = "Giày thể thao thoáng khí, nhẹ nhàng.", TrangThai = "Còn hàng" }
            );
        }
    }
} 