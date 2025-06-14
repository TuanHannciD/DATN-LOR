using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class TenMigrationMoi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_SanPhamChiTiet_Gia",
                table: "SanPhamChiTiets");

            migrationBuilder.DropCheckConstraint(
                name: "CK_SanPhamChiTiet_SoLuongBan",
                table: "SanPhamChiTiets");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NhanVien_GioiTinh",
                table: "NhanViens");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NhanVien_LuongCoBan",
                table: "NhanViens");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NhanVien_NgaySinh",
                table: "NhanViens");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NhanVien_SoGioLamViec",
                table: "NhanViens");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NhanVien_TrangThai",
                table: "NhanViens");

            migrationBuilder.DropCheckConstraint(
                name: "CK_NCC_TrangThai",
                table: "NCCs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhuyenMai_GiaTriKm",
                table: "KhuyenMais");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhuyenMai_LoaiKm",
                table: "KhuyenMais");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhuyenMai_SoLuong",
                table: "KhuyenMais");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhuyenMai_SoLuong1Ng",
                table: "KhuyenMais");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_DiemTichLuy",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_Email",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_GioiTinh",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_HoTen",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_LoaiKhachHang",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_NgaySinh",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_KhachHang_TrangThai",
                table: "KhachHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_GiaoHang_TrangThaiGiaoHang",
                table: "GiaoHangs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_DoanhThu",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_LoaiBaoCao",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoKhachHangMoi",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoKhachHangTroLai",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangBiHuy",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangDangXuLy",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangHoanThanh",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoLuongHangBanRa",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_SoLuongHangTon",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_TongSoDonHang",
                table: "BaoCaos");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BaoCao_TongSoKhachHang",
                table: "BaoCaos");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Role" },
                values: new object[] { "testuser", "123456", "User" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "HoTenAdmin", "AnhDaiDien", "DiaChi", "Email", "NgaySinh", "Sdt", "UserName" },
                values: new object[] { "Admin Demo", "avatar.png", "Hà Nội", "admin@gmail.com", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "testuser" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Admins",
                keyColumn: "HoTenAdmin",
                keyValue: "Admin Demo");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Username",
                keyValue: "testuser");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SanPhamChiTiet_Gia",
                table: "SanPhamChiTiets",
                sql: "Gia > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_SanPhamChiTiet_SoLuongBan",
                table: "SanPhamChiTiets",
                sql: "SoLuongBan >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NhanVien_GioiTinh",
                table: "NhanViens",
                sql: "GioiTinh IN (N'Nam', N'Nữ', N'Khác')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NhanVien_LuongCoBan",
                table: "NhanViens",
                sql: "LuongCoBan > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NhanVien_NgaySinh",
                table: "NhanViens",
                sql: "NgaySinh <= GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NhanVien_SoGioLamViec",
                table: "NhanViens",
                sql: "SoGioLamViec >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NhanVien_TrangThai",
                table: "NhanViens",
                sql: "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_NCC_TrangThai",
                table: "NCCs",
                sql: "TrangThai IN (N'Hoạt động', N'Không hoạt động')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhuyenMai_GiaTriKm",
                table: "KhuyenMais",
                sql: "GiaTriKm > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhuyenMai_LoaiKm",
                table: "KhuyenMais",
                sql: "LoaiKm IN (N'Phần trăm', N'Số tiền cố định', N'Miễn phí giao hàng')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhuyenMai_SoLuong",
                table: "KhuyenMais",
                sql: "SoLuong >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhuyenMai_SoLuong1Ng",
                table: "KhuyenMais",
                sql: "SoLuong1Ng >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_DiemTichLuy",
                table: "KhachHangs",
                sql: "DiemTichLuy >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_Email",
                table: "KhachHangs",
                sql: "Email LIKE N'%_@gmail.com'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_GioiTinh",
                table: "KhachHangs",
                sql: "GioiTinh IN (N'Nam', N'Nữ', N'Khác')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_HoTen",
                table: "KhachHangs",
                sql: "HoTen LIKE N'%[^a-zA-Z]% '");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_LoaiKhachHang",
                table: "KhachHangs",
                sql: "LoaiKhachHang IN (N'VIP', N'Thường', N'Mới')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_NgaySinh",
                table: "KhachHangs",
                sql: "NgaySinh <= GETDATE()");

            migrationBuilder.AddCheckConstraint(
                name: "CK_KhachHang_TrangThai",
                table: "KhachHangs",
                sql: "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_GiaoHang_TrangThaiGiaoHang",
                table: "GiaoHangs",
                sql: "TrangThaiGiaoHang IN (N'Chưa phân công', N'Đang giao', N'Đã giao', N'Đang xử lý', N'Đang chờ giao hàng', N'Gặp sự cố', N'Bị hủy', N'Giao lại')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_DoanhThu",
                table: "BaoCaos",
                sql: "DoanhThu >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_LoaiBaoCao",
                table: "BaoCaos",
                sql: "LoaiBaoCao IN (N'DoanhSo', N'HangTon', N'DonHang', N'KhachHang')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoKhachHangMoi",
                table: "BaoCaos",
                sql: "SoKhachHangMoi >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoKhachHangTroLai",
                table: "BaoCaos",
                sql: "SoKhachHangTroLai >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangBiHuy",
                table: "BaoCaos",
                sql: "SoLuongDonHangBiHuy >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangDangXuLy",
                table: "BaoCaos",
                sql: "SoLuongDonHangDangXuLy >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoLuongDonHangHoanThanh",
                table: "BaoCaos",
                sql: "SoLuongDonHangHoanThanh >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoLuongHangBanRa",
                table: "BaoCaos",
                sql: "SoLuongHangBanRa >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_SoLuongHangTon",
                table: "BaoCaos",
                sql: "SoLuongHangTon >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_TongSoDonHang",
                table: "BaoCaos",
                sql: "TongSoDonHang >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_BaoCao_TongSoKhachHang",
                table: "BaoCaos",
                sql: "TongSoKhachHang >= 0");
        }
    }
}
