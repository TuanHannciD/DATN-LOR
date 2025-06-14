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
                new SanPham { ID_Sp = Guid.NewGuid(), Ten_Sp = "Áo thun cơ bản", SoLuongTong = 100, MoTa = "Áo thun cổ tròn, vải cotton mềm mại.", TrangThai = "Còn hàng" },
                new SanPham { ID_Sp = Guid.NewGuid(), Ten_Sp = "Quần jean slim fit", SoLuongTong = 50, MoTa = "Quần jean dáng ôm, co giãn tốt.", TrangThai = "Còn hàng" },
                new SanPham { ID_Sp = Guid.NewGuid(), Ten_Sp = "Giày thể thao A", SoLuongTong = 75, MoTa = "Giày thể thao thoáng khí, nhẹ nhàng.", TrangThai = "Còn hàng" }
            );
        }
    }
} 