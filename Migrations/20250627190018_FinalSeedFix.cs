using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLieus",
                columns: table => new
                {
                    ID_ChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatLieuName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieus", x => x.ID_ChatLieu);
                });

            migrationBuilder.CreateTable(
                name: "HangSXs",
                columns: table => new
                {
                    ID_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HangSXName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HangSXs", x => x.ID_Hang);
                });

            migrationBuilder.CreateTable(
                name: "MauSacs",
                columns: table => new
                {
                    ID_MauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MauSacName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSacs", x => x.ID_MauSac);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPhams",
                columns: table => new
                {
                    ID_Sp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten_Sp = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoLuongTong = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhams", x => x.ID_Sp);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    ID_Size = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.ID_Size);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<int>(type: "int", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiets",
                columns: table => new
                {
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Sp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SoLuongBan = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DanhGia = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ID_ChatLieu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_MauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ID_Size = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiets", x => x.ID_Spct);
                    table.CheckConstraint("CK_SanPhamChiTiet_Gia", "Gia > 0");
                    table.CheckConstraint("CK_SanPhamChiTiet_SoLuongBan", "SoLuongBan >= 0");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_ChatLieus_ID_ChatLieu",
                        column: x => x.ID_ChatLieu,
                        principalTable: "ChatLieus",
                        principalColumn: "ID_ChatLieu",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_HangSXs_ID_Hang",
                        column: x => x.ID_Hang,
                        principalTable: "HangSXs",
                        principalColumn: "ID_Hang",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_MauSacs_ID_MauSac",
                        column: x => x.ID_MauSac,
                        principalTable: "MauSacs",
                        principalColumn: "ID_MauSac",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_SanPhams_ID_Sp",
                        column: x => x.ID_Sp,
                        principalTable: "SanPhams",
                        principalColumn: "ID_Sp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_Sizes_ID_Size",
                        column: x => x.ID_Size,
                        principalTable: "Sizes",
                        principalColumn: "ID_Size",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.HoTenAdmin);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnhSps",
                columns: table => new
                {
                    ID_AnhSp = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileAnh = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnhSps", x => x.ID_AnhSp);
                    table.ForeignKey(
                        name: "FK_AnhSps_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TonKhos",
                columns: table => new
                {
                    ID_TonKho = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuongTonKho = table.Column<int>(type: "int", nullable: false),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonKhos", x => x.ID_TonKho);
                    table.ForeignKey(
                        name: "FK_TonKhos_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaoCaos",
                columns: table => new
                {
                    ID_BaoCao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayBaoCao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiBaoCao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoanhThu = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SoLuongHangBanRa = table.Column<int>(type: "int", nullable: true),
                    SoLuongHangTon = table.Column<int>(type: "int", nullable: true),
                    TongSoDonHang = table.Column<int>(type: "int", nullable: true),
                    SoLuongDonHangHoanThanh = table.Column<int>(type: "int", nullable: true),
                    SoLuongDonHangDangXuLy = table.Column<int>(type: "int", nullable: true),
                    SoLuongDonHangBiHuy = table.Column<int>(type: "int", nullable: true),
                    TongSoKhachHang = table.Column<int>(type: "int", nullable: true),
                    SoKhachHangMoi = table.Column<int>(type: "int", nullable: true),
                    SoKhachHangTroLai = table.Column<int>(type: "int", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaos", x => x.ID_BaoCao);
                    table.CheckConstraint("CK_BaoCao_DoanhThu", "DoanhThu >= 0");
                    table.CheckConstraint("CK_BaoCao_LoaiBaoCao", "LoaiBaoCao IN (N'DoanhSo', N'HangTon', N'DonHang', N'KhachHang')");
                    table.CheckConstraint("CK_BaoCao_SoKhachHangMoi", "SoKhachHangMoi >= 0");
                    table.CheckConstraint("CK_BaoCao_SoKhachHangTroLai", "SoKhachHangTroLai >= 0");
                    table.CheckConstraint("CK_BaoCao_SoLuongDonHangBiHuy", "SoLuongDonHangBiHuy >= 0");
                    table.CheckConstraint("CK_BaoCao_SoLuongDonHangDangXuLy", "SoLuongDonHangDangXuLy >= 0");
                    table.CheckConstraint("CK_BaoCao_SoLuongDonHangHoanThanh", "SoLuongDonHangHoanThanh >= 0");
                    table.CheckConstraint("CK_BaoCao_SoLuongHangBanRa", "SoLuongHangBanRa >= 0");
                    table.CheckConstraint("CK_BaoCao_SoLuongHangTon", "SoLuongHangTon >= 0");
                    table.CheckConstraint("CK_BaoCao_TongSoDonHang", "TongSoDonHang >= 0");
                    table.CheckConstraint("CK_BaoCao_TongSoKhachHang", "TongSoKhachHang >= 0");
                    table.ForeignKey(
                        name: "FK_BaoCaos_Admins_HoTenAdmin",
                        column: x => x.HoTenAdmin,
                        principalTable: "Admins",
                        principalColumn: "HoTenAdmin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgayDangKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiemTichLuy = table.Column<int>(type: "int", nullable: false),
                    LoaiKhachHang = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.HoTen);
                    table.UniqueConstraint("AK_KhachHangs_HoTen_Email", x => new { x.HoTen, x.Email });
                    table.CheckConstraint("CK_KhachHang_DiemTichLuy", "DiemTichLuy >= 0");
                    table.CheckConstraint("CK_KhachHang_Email", "Email LIKE N'%_@gmail.com'");
                    table.CheckConstraint("CK_KhachHang_GioiTinh", "GioiTinh IN (N'Nam', N'Nữ', N'Khác')");
                    table.CheckConstraint("CK_KhachHang_HoTen", "HoTen LIKE N'%[^a-zA-Z]% '");
                    table.CheckConstraint("CK_KhachHang_LoaiKhachHang", "LoaiKhachHang IN (N'VIP', N'Thường', N'Mới')");
                    table.CheckConstraint("CK_KhachHang_NgaySinh", "NgaySinh <= GETDATE()");
                    table.CheckConstraint("CK_KhachHang_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')");
                    table.ForeignKey(
                        name: "FK_KhachHangs_Admins_HoTenAdmin",
                        column: x => x.HoTenAdmin,
                        principalTable: "Admins",
                        principalColumn: "HoTenAdmin",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_KhachHangs_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMais",
                columns: table => new
                {
                    Ma_Km = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    LoaiKm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GiaTriKm = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    NgayBd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    SoLuong1Ng = table.Column<int>(type: "int", nullable: true),
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMais", x => x.Ma_Km);
                    table.CheckConstraint("CK_KhuyenMai_GiaTriKm", "GiaTriKm > 0");
                    table.CheckConstraint("CK_KhuyenMai_LoaiKm", "LoaiKm IN (N'Phần trăm', N'Số tiền cố định', N'Miễn phí giao hàng')");
                    table.CheckConstraint("CK_KhuyenMai_SoLuong", "SoLuong >= 0");
                    table.CheckConstraint("CK_KhuyenMai_SoLuong1Ng", "SoLuong1Ng >= 0");
                    table.ForeignKey(
                        name: "FK_KhuyenMais_Admins_HoTenAdmin",
                        column: x => x.HoTenAdmin,
                        principalTable: "Admins",
                        principalColumn: "HoTenAdmin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCCs",
                columns: table => new
                {
                    Ma_NCC = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameNCC = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NameNLH = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ThanhPho = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuocGia = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCCs", x => x.Ma_NCC);
                    table.CheckConstraint("CK_NCC_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động')");
                    table.ForeignKey(
                        name: "FK_NCCs_Admins_HoTenAdmin",
                        column: x => x.HoTenAdmin,
                        principalTable: "Admins",
                        principalColumn: "HoTenAdmin",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTenNV = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    HoTenAdmin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NgayVaoLam = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LuongCoBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoGioLamViec = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNV);
                    table.CheckConstraint("CK_NhanVien_GioiTinh", "GioiTinh IN (N'Nam', N'Nữ', N'Khác')");
                    table.CheckConstraint("CK_NhanVien_LuongCoBan", "LuongCoBan > 0");
                    table.CheckConstraint("CK_NhanVien_NgaySinh", "NgaySinh <= GETDATE()");
                    table.CheckConstraint("CK_NhanVien_SoGioLamViec", "SoGioLamViec >= 0");
                    table.CheckConstraint("CK_NhanVien_TrangThai", "TrangThai IN (N'Hoạt động', N'Không hoạt động', N'Bị cấm')");
                    table.ForeignKey(
                        name: "FK_NhanViens_Admins_HoTenAdmin",
                        column: x => x.HoTenAdmin,
                        principalTable: "Admins",
                        principalColumn: "HoTenAdmin",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NhanViens_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_KhachHangs",
                columns: table => new
                {
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_KhachHangs", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_User_KhachHangs_KhachHangs_HoTen_Email",
                        columns: x => new { x.HoTen, x.Email },
                        principalTable: "KhachHangs",
                        principalColumns: new[] { "HoTen", "Email" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_KhachHangs_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SanPham_Muas",
                columns: table => new
                {
                    ID_SP_Mua = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoLuong = table.Column<float>(type: "real", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    Ma_Km = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham_Muas", x => x.ID_SP_Mua);
                    table.ForeignKey(
                        name: "FK_SanPham_Muas_KhuyenMais_Ma_Km",
                        column: x => x.Ma_Km,
                        principalTable: "KhuyenMais",
                        principalColumn: "Ma_Km",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_Muas_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_Muas_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "DonHangs",
                columns: table => new
                {
                    ID_Don_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHangs", x => x.ID_Don_Hang);
                    table.ForeignKey(
                        name: "FK_DonHangs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gio_Hangs",
                columns: table => new
                {
                    ID_Gio_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gio_Hangs", x => x.ID_Gio_Hang);
                    table.ForeignKey(
                        name: "FK_Gio_Hangs_User_KhachHangs_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User_KhachHangs",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    ID_HoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.ID_HoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_User_KhachHangs_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User_KhachHangs",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoTroKhachHangs",
                columns: table => new
                {
                    ID_HoTroKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoaiHT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PTLienLac = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoTroKhachHangs", x => x.ID_HoTroKhachHang);
                    table.ForeignKey(
                        name: "FK_HoTroKhachHangs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoTroKhachHangs_User_KhachHangs_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User_KhachHangs",
                        principalColumn: "ID_User");
                });

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThiches",
                columns: table => new
                {
                    ID_Spyt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThiches", x => x.ID_Spyt);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThiches_User_KhachHangs_ID_User",
                        column: x => x.ID_User,
                        principalTable: "User_KhachHangs",
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Gio_Hang_Chi_Tiets",
                columns: table => new
                {
                    ID_Gio_Hang_Chi_Tiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Gio_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma_Km = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<float>(type: "real", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gio_Hang_Chi_Tiets", x => x.ID_Gio_Hang_Chi_Tiet);
                    table.ForeignKey(
                        name: "FK_Gio_Hang_Chi_Tiets_Gio_Hangs_ID_Gio_Hang",
                        column: x => x.ID_Gio_Hang,
                        principalTable: "Gio_Hangs",
                        principalColumn: "ID_Gio_Hang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Gio_Hang_Chi_Tiets_KhuyenMais_Ma_Km",
                        column: x => x.Ma_Km,
                        principalTable: "KhuyenMais",
                        principalColumn: "Ma_Km");
                    table.ForeignKey(
                        name: "FK_Gio_Hang_Chi_Tiets_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToans",
                columns: table => new
                {
                    ID_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_HoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SoTienThanhToan = table.Column<float>(type: "real", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SDT = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToans", x => x.ID_ThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToans_HoaDons_ID_HoaDon",
                        column: x => x.ID_HoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "ID_HoaDon",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamYeuThichChiTiets",
                columns: table => new
                {
                    ID_Spyt_Chi_Tiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spyt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamYeuThichChiTiets", x => x.ID_Spyt_Chi_Tiet);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThichChiTiets_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPhamYeuThichChiTiets_SanPhamYeuThiches_ID_Spyt",
                        column: x => x.ID_Spyt,
                        principalTable: "SanPhamYeuThiches",
                        principalColumn: "ID_Spyt",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Don_Hang_Thanh_Toans",
                columns: table => new
                {
                    ID_Don_Hang_Thanh_Toan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Don_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NgayTT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    KieuTT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Don_Hang_Thanh_Toans", x => x.ID_Don_Hang_Thanh_Toan);
                    table.ForeignKey(
                        name: "FK_Don_Hang_Thanh_Toans_DonHangs_ID_Don_Hang",
                        column: x => x.ID_Don_Hang,
                        principalTable: "DonHangs",
                        principalColumn: "ID_Don_Hang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Don_Hang_Thanh_Toans_ThanhToans_ID_ThanhToan",
                        column: x => x.ID_ThanhToan,
                        principalTable: "ThanhToans",
                        principalColumn: "ID_ThanhToan");
                });

            migrationBuilder.CreateTable(
                name: "GiaoHangs",
                columns: table => new
                {
                    ID_GiaoHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Don_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NgayPhanCongGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ThoiGianDuKienGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianThucTeGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiGiaoHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoHangs", x => x.ID_GiaoHang);
                    table.CheckConstraint("CK_GiaoHang_TrangThaiGiaoHang", "TrangThaiGiaoHang IN (N'Chưa phân công', N'Đang giao', N'Đã giao', N'Đang xử lý', N'Đang chờ giao hàng', N'Gặp sự cố', N'Bị hủy', N'Giao lại')");
                    table.ForeignKey(
                        name: "FK_GiaoHangs_DonHangs_ID_Don_Hang",
                        column: x => x.ID_Don_Hang,
                        principalTable: "DonHangs",
                        principalColumn: "ID_Don_Hang",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GiaoHangs_ThanhToans_ID_ThanhToan",
                        column: x => x.ID_ThanhToan,
                        principalTable: "ThanhToans",
                        principalColumn: "ID_ThanhToan");
                });

            migrationBuilder.CreateTable(
                name: "SanPham_ThanhToans",
                columns: table => new
                {
                    ID_Sp_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham_ThanhToans", x => x.ID_Sp_ThanhToan);
                    table.ForeignKey(
                        name: "FK_SanPham_ThanhToans_SanPhamChiTiets_ID_Spct",
                        column: x => x.ID_Spct,
                        principalTable: "SanPhamChiTiets",
                        principalColumn: "ID_Spct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SanPham_ThanhToans_ThanhToans_ID_ThanhToan",
                        column: x => x.ID_ThanhToan,
                        principalTable: "ThanhToans",
                        principalColumn: "ID_ThanhToan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChatLieus",
                columns: new[] { "ID_ChatLieu", "ChatLieuName" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), "Cotton" },
                    { new Guid("8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d"), "Len" }
                });

            migrationBuilder.InsertData(
                table: "HangSXs",
                columns: new[] { "ID_Hang", "HangSXName" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"), "Nike" },
                    { new Guid("f1e2d3c4-b5a6-9d8e-7c6b-5a4d3c2b1a0e"), "Adidas" }
                });

            migrationBuilder.InsertData(
                table: "MauSacs",
                columns: new[] { "ID_MauSac", "MauSacName" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), "Đen" },
                    { new Guid("11223344-5566-7788-99aa-bbccddeeff11"), "Vàng" },
                    { new Guid("22334455-6677-8899-aabb-ccddeeff2233"), "Xanh" },
                    { new Guid("66666666-7777-8888-9999-aaaaaaaaaaaa"), "Trắng" },
                    { new Guid("b1a7e8c2-3f4d-4e6a-9b2c-1a2b3c4d5e6f"), "Đỏ" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "User" },
                    { 2, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SanPhams",
                columns: new[] { "ID_Sp", "MoTa", "SoLuongTong", "Ten_Sp", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("33333333-4444-5555-6666-777777777777"), "Giày thể thao thoáng khí, nhẹ nhàng.", 75, "Giày thể thao A", "Còn hàng" },
                    { new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), "Áo thun cổ tròn, vải cotton mềm mại.", 100, "Áo thun cơ bản", "Còn hàng" },
                    { new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), "Quần jean dáng ôm, co giãn tốt.", 50, "Quần jean slim fit", "Còn hàng" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "ID_Size", "SizeName" },
                values: new object[,]
                {
                    { new Guid("eeddccbb-aa99-8877-6655-443322110000"), "M" },
                    { new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), "S" }
                });

            migrationBuilder.InsertData(
                table: "SanPhamChiTiets",
                columns: new[] { "ID_Spct", "AnhDaiDien", "DanhGia", "Gia", "ID_ChatLieu", "ID_Hang", "ID_MauSac", "ID_Size", "ID_Sp", "MoTa", "SoLuongBan", "TenSp" },
                values: new object[,]
                {
                    { new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), "ao_thun_dac_biet.jpg", "Xuất sắc", 250000f, new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"), new Guid("11111111-2222-3333-4444-555555555555"), new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), "Áo thun đặc biệt, vải cao cấp, màu đen, size M.", 20, "Áo thun đặc biệt" },
                    { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), "ao_thun_do_s.jpg", "Tốt", 150000f, new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6e"), new Guid("11223344-5566-7788-99aa-bbccddeeff11"), new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), "Áo thun cổ tròn, vải cotton mềm mại, màu đỏ, size S.", 10, "Áo thun cơ bản Đỏ S" },
                    { new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), "quan_jean_xanh_m.jpg", "Tuyệt vời", 450000f, new Guid("8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d"), new Guid("f1e2d3c4-b5a6-9d8e-7c6b-5a4d3c2b1a0e"), new Guid("22334455-6677-8899-aabb-ccddeeff2233"), new Guid("eeddccbb-aa99-8877-6655-443322110000"), new Guid("b1c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), "Quần jean dáng ôm, co giãn tốt, màu xanh, size M.", 5, "Quần jean slim fit Xanh M" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "RoleId" },
                values: new object[] { "testuser", "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=", 1 });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "HoTenAdmin", "AnhDaiDien", "DiaChi", "Email", "NgaySinh", "Sdt", "UserName" },
                values: new object[] { "Admin Demo", "avatar.png", "Hà Nội", "admin@gmail.com", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0123456789", "testuser" });

            migrationBuilder.InsertData(
                table: "AnhSps",
                columns: new[] { "ID_AnhSp", "FileAnh", "ID_Spct" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "ao_thun_do_s_1.jpg", new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "ao_thun_do_s_2.jpg", new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "quan_jean_xanh_m_1.jpg", new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff") }
                });

            migrationBuilder.InsertData(
                table: "TonKhos",
                columns: new[] { "ID_TonKho", "ID_Spct", "NgayCapNhap", "SoLuongTonKho" },
                values: new object[] { new Guid("bbbbbbbb-cccc-dddd-eeee-ffffffffffff"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 50 });

            migrationBuilder.InsertData(
                table: "BaoCaos",
                columns: new[] { "ID_BaoCao", "DoanhThu", "HoTenAdmin", "LoaiBaoCao", "NgayBaoCao", "NgayCapNhap", "NgayTao", "SoKhachHangMoi", "SoKhachHangTroLai", "SoLuongDonHangBiHuy", "SoLuongDonHangDangXuLy", "SoLuongDonHangHoanThanh", "SoLuongHangBanRa", "SoLuongHangTon", "TongSoDonHang", "TongSoKhachHang" },
                values: new object[] { new Guid("cccccccc-dddd-eeee-ffff-111111111111"), 10000000.0m, "Admin Demo", "DoanhSo", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 20, 180, 5, 15, 80, 500, 2000, 100, 200 });

            migrationBuilder.InsertData(
                table: "KhachHangs",
                columns: new[] { "HoTen", "DiaChi", "DiemTichLuy", "Email", "GioiTinh", "HoTenAdmin", "LoaiKhachHang", "NgayDangKy", "NgaySinh", "Sdt", "TrangThai", "UserName" },
                values: new object[] { "Tran Thi B ", "Hà Nội", 0, "khachhangB@gmail.com", "Nữ", "Admin Demo", "Thường", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", "Hoạt động", "testuser" });

            migrationBuilder.InsertData(
                table: "KhuyenMais",
                columns: new[] { "Ma_Km", "GiaTriKm", "HoTenAdmin", "LoaiKm", "MoTa", "NgayBd", "NgayKt", "SoLuong", "SoLuong1Ng", "TenKm" },
                values: new object[,]
                {
                    { new Guid("dddddddd-eeee-ffff-1111-222222222222"), 10.0m, "Admin Demo", "Phần trăm", "Giảm 10% cho tất cả sản phẩm", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000, 100, "Giảm giá mùa hè" },
                    { new Guid("eeeeeeee-ffff-1111-2222-333333333333"), 12m, "Admin Demo", "Miễn phí giao hàng", "Miễn phí giao hàng cho đơn hàng trên 500k", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 500, 50, "Miễn phí vận chuyển" },
                    { new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f"), 50000.0m, "Admin Demo", "Số tiền cố định", "Voucher giảm 50k cho đơn hàng trên 300k", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 5, "Tặng voucher đặc biệt" }
                });

            migrationBuilder.InsertData(
                table: "NCCs",
                columns: new[] { "Ma_NCC", "CreatedDate", "DiaChi", "Email", "HoTenAdmin", "MoTa", "NameNCC", "NameNLH", "QuocGia", "SDT", "ThanhPho", "TrangThai", "UpdatedDate" },
                values: new object[] { new Guid("22222222-3333-4444-5555-666666666666"), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hà Nội", "nccA@example.com", "Admin Demo", "Chuyên cung cấp vải cotton", "Nhà cung cấp A", "Nguyen Van Test", "Việt Nam", "0123456789", "Hà Nội", "Hoạt động", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "NhanViens",
                columns: new[] { "MaNV", "ChucVu", "DiaChi", "Email", "GioiTinh", "HoTenAdmin", "HoTenNV", "LuongCoBan", "NgaySinh", "NgayVaoLam", "Sdt", "SoGioLamViec", "TrangThai", "UserName" },
                values: new object[,]
                {
                    { new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), "Nhân viên", "Hà Nội", "nhanvienB@example.com", "Nam", "Admin Demo", "Nguyen Van B", 12000000m, new DateTime(1996, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678", 160, "Hoạt động", "testuser" },
                    { new Guid("ffffffff-1111-2222-3333-444444444444"), "Quản lý", "Hà Nội", "nhanvienA@example.com", "Nam", "Admin Demo", "Nguyen Van A", 15000000m, new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", 160, "Hoạt động", "testuser" }
                });

            migrationBuilder.InsertData(
                table: "DonHangs",
                columns: new[] { "ID_Don_Hang", "MaNV" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c") },
                    { new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c") }
                });

            migrationBuilder.InsertData(
                table: "SanPham_Muas",
                columns: new[] { "ID_SP_Mua", "Gia", "ID_Spct", "Ma_Km", "SoLuong", "UserName" },
                values: new object[] { new Guid("44444444-4444-4444-4444-444444444444"), 150000f, new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f"), 1f, "testuser" });

            migrationBuilder.InsertData(
                table: "User_KhachHangs",
                columns: new[] { "ID_User", "Email", "HoTen", "UserName" },
                values: new object[] { new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), "khachhangB@gmail.com", "Tran Thi B ", "testuser" });

            migrationBuilder.InsertData(
                table: "Gio_Hangs",
                columns: new[] { "ID_Gio_Hang", "ID_User" },
                values: new object[,]
                {
                    { new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") },
                    { new Guid("b2c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") }
                });

            migrationBuilder.InsertData(
                table: "HoTroKhachHangs",
                columns: new[] { "ID_HoTroKhachHang", "ID_User", "LoaiHT", "MaNV", "PTLienLac" },
                values: new object[] { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), "Hỗ trợ kỹ thuật", new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), "Email" });

            migrationBuilder.InsertData(
                table: "HoaDons",
                columns: new[] { "ID_HoaDon", "ID_User" },
                values: new object[,]
                {
                    { new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") },
                    { new Guid("c3c7d81c-7d81-4e9c-8e0a-0a4a8b0e7a2b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") }
                });

            migrationBuilder.InsertData(
                table: "SanPhamYeuThiches",
                columns: new[] { "ID_Spyt", "ID_User" },
                values: new object[] { new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") });

            migrationBuilder.InsertData(
                table: "Gio_Hang_Chi_Tiets",
                columns: new[] { "ID_Gio_Hang_Chi_Tiet", "Gia", "ID_Gio_Hang", "ID_Spct", "Ma_Km", "SoLuong" },
                values: new object[,]
                {
                    { new Guid("66666666-6666-6666-6666-666666666666"), 150000f, new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), new Guid("f8e1d2c3-b4a5-6d7e-8c9b-0a1b2c3d4e5f"), 1f },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 450000f, new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), null, 2f }
                });

            migrationBuilder.InsertData(
                table: "SanPhamYeuThichChiTiets",
                columns: new[] { "ID_Spyt_Chi_Tiet", "Gia", "ID_Spct", "ID_Spyt" },
                values: new object[] { new Guid("99999999-9999-9999-9999-999999999999"), 150000f, new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3b") });

            migrationBuilder.InsertData(
                table: "ThanhToans",
                columns: new[] { "ID_ThanhToan", "DiaChi", "GhiChu", "HoTen", "ID_HoaDon", "PhuongThucThanhToan", "SDT", "SoTienThanhToan", "Status" },
                values: new object[,]
                {
                    { new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), "Hà Nội", "Thanh toán đơn hàng #2", "Nguyen Van A", new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), "Chuyển khoản", "0912345678", 150000f, "Đã thanh toán" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Hà Nội", "Thanh toán đơn hàng #1", "Tran Thi B", new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), "Chuyển khoản", "0912345678", 150000f, "Đã thanh toán" }
                });

            migrationBuilder.InsertData(
                table: "Don_Hang_Thanh_Toans",
                columns: new[] { "ID_Don_Hang_Thanh_Toan", "ID_Don_Hang", "ID_ThanhToan", "KieuTT", "NgayTT", "Status" },
                values: new object[] { new Guid("11111111-aaaa-bbbb-cccc-555555555555"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), "Tiền mặt", new DateTime(2025, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hoàn thành" });

            migrationBuilder.InsertData(
                table: "GiaoHangs",
                columns: new[] { "ID_GiaoHang", "ID_Don_Hang", "ID_ThanhToan", "NgayCapNhap", "NgayPhanCongGiaoHang", "NgayTao", "ThoiGianDuKienGiaoHang", "ThoiGianThucTeGiaoHang", "TrangThaiGiaoHang" },
                values: new object[] { new Guid("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2c"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Đang xử lý" });

            migrationBuilder.InsertData(
                table: "SanPham_ThanhToans",
                columns: new[] { "ID_Sp_ThanhToan", "ID_Spct", "ID_ThanhToan", "SoLuong" },
                values: new object[] { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("a0e6c70b-6c4a-4b9e-9d2a-0a4a8b0e7a2b"), new Guid("ae0f3e6e-21a4-4f5b-9d2c-8a7e6f5d4c3a"), 1f });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserName",
                table: "Admins",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AnhSps_ID_Spct",
                table: "AnhSps",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaos_HoTenAdmin",
                table: "BaoCaos",
                column: "HoTenAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Don_Hang_Thanh_Toans_ID_Don_Hang",
                table: "Don_Hang_Thanh_Toans",
                column: "ID_Don_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_Don_Hang_Thanh_Toans_ID_ThanhToan",
                table: "Don_Hang_Thanh_Toans",
                column: "ID_ThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_DonHangs_MaNV",
                table: "DonHangs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoHangs_ID_Don_Hang",
                table: "GiaoHangs",
                column: "ID_Don_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoHangs_ID_ThanhToan",
                table: "GiaoHangs",
                column: "ID_ThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_Gio_Hang_Chi_Tiets_ID_Gio_Hang",
                table: "Gio_Hang_Chi_Tiets",
                column: "ID_Gio_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_Gio_Hang_Chi_Tiets_ID_Spct",
                table: "Gio_Hang_Chi_Tiets",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_Gio_Hang_Chi_Tiets_Ma_Km",
                table: "Gio_Hang_Chi_Tiets",
                column: "Ma_Km");

            migrationBuilder.CreateIndex(
                name: "IX_Gio_Hangs_ID_User",
                table: "Gio_Hangs",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_ID_User",
                table: "HoaDons",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_HoTroKhachHangs_ID_User",
                table: "HoTroKhachHangs",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_HoTroKhachHangs_MaNV",
                table: "HoTroKhachHangs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_HoTenAdmin",
                table: "KhachHangs",
                column: "HoTenAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHangs_UserName",
                table: "KhachHangs",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMais_HoTenAdmin",
                table: "KhuyenMais",
                column: "HoTenAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_NCCs_HoTenAdmin",
                table: "NCCs",
                column: "HoTenAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_HoTenAdmin",
                table: "NhanViens",
                column: "HoTenAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_UserName",
                table: "NhanViens",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_Muas_ID_Spct",
                table: "SanPham_Muas",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_Muas_Ma_Km",
                table: "SanPham_Muas",
                column: "Ma_Km");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_Muas_UserName",
                table: "SanPham_Muas",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ThanhToans_ID_Spct",
                table: "SanPham_ThanhToans",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_ThanhToans_ID_ThanhToan",
                table: "SanPham_ThanhToans",
                column: "ID_ThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_ID_ChatLieu",
                table: "SanPhamChiTiets",
                column: "ID_ChatLieu");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_ID_Hang",
                table: "SanPhamChiTiets",
                column: "ID_Hang");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_ID_MauSac",
                table: "SanPhamChiTiets",
                column: "ID_MauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_ID_Size",
                table: "SanPhamChiTiets",
                column: "ID_Size");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiets_ID_Sp",
                table: "SanPhamChiTiets",
                column: "ID_Sp");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThichChiTiets_ID_Spct",
                table: "SanPhamYeuThichChiTiets",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThichChiTiets_ID_Spyt",
                table: "SanPhamYeuThichChiTiets",
                column: "ID_Spyt");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamYeuThiches_ID_User",
                table: "SanPhamYeuThiches",
                column: "ID_User");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToans_ID_HoaDon",
                table: "ThanhToans",
                column: "ID_HoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_TonKhos_ID_Spct",
                table: "TonKhos",
                column: "ID_Spct");

            migrationBuilder.CreateIndex(
                name: "IX_User_KhachHangs_HoTen_Email",
                table: "User_KhachHangs",
                columns: new[] { "HoTen", "Email" });

            migrationBuilder.CreateIndex(
                name: "IX_User_KhachHangs_UserName",
                table: "User_KhachHangs",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhSps");

            migrationBuilder.DropTable(
                name: "BaoCaos");

            migrationBuilder.DropTable(
                name: "Don_Hang_Thanh_Toans");

            migrationBuilder.DropTable(
                name: "GiaoHangs");

            migrationBuilder.DropTable(
                name: "Gio_Hang_Chi_Tiets");

            migrationBuilder.DropTable(
                name: "HoTroKhachHangs");

            migrationBuilder.DropTable(
                name: "NCCs");

            migrationBuilder.DropTable(
                name: "SanPham_Muas");

            migrationBuilder.DropTable(
                name: "SanPham_ThanhToans");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThichChiTiets");

            migrationBuilder.DropTable(
                name: "TonKhos");

            migrationBuilder.DropTable(
                name: "DonHangs");

            migrationBuilder.DropTable(
                name: "Gio_Hangs");

            migrationBuilder.DropTable(
                name: "KhuyenMais");

            migrationBuilder.DropTable(
                name: "ThanhToans");

            migrationBuilder.DropTable(
                name: "SanPhamYeuThiches");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiets");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "ChatLieus");

            migrationBuilder.DropTable(
                name: "HangSXs");

            migrationBuilder.DropTable(
                name: "MauSacs");

            migrationBuilder.DropTable(
                name: "SanPhams");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "User_KhachHangs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
