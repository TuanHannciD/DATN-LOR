using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class CreateFullDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    HoTen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => new { x.Email, x.HoTen });
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
                        principalColumn: "HoTenAdmin");
                    table.ForeignKey(
                        name: "FK_KhachHangs_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                        principalColumn: "ID_ChatLieu");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_HangSXs_ID_Hang",
                        column: x => x.ID_Hang,
                        principalTable: "HangSXs",
                        principalColumn: "ID_Hang");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiets_MauSacs_ID_MauSac",
                        column: x => x.ID_MauSac,
                        principalTable: "MauSacs",
                        principalColumn: "ID_MauSac");
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
                        principalColumn: "ID_Size");
                });

            migrationBuilder.CreateTable(
                name: "User_KhachHangs",
                columns: table => new
                {
                    ID_User = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KhachHangEmail = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    KhachHangHoTen = table.Column<string>(type: "nvarchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_KhachHangs", x => x.ID_User);
                    table.ForeignKey(
                        name: "FK_User_KhachHangs_KhachHangs_KhachHangEmail_KhachHangHoTen",
                        columns: x => new { x.KhachHangEmail, x.KhachHangHoTen },
                        principalTable: "KhachHangs",
                        principalColumns: new[] { "Email", "HoTen" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_KhachHangs_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
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
                        principalColumn: "ID_Spct");
                });

            migrationBuilder.CreateTable(
                name: "SanPham_Muas",
                columns: table => new
                {
                    ID_SP_Mua = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
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
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TonKhos",
                columns: table => new
                {
                    ID_TonKho = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Spct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuongTonKho = table.Column<int>(type: "int", nullable: false),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        principalColumn: "ID_User",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "ID_ThanhToan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaoHangs",
                columns: table => new
                {
                    ID_GiaoHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_ThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_Don_Hang = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NgayPhanCongGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianDuKienGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianThucTeGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiGiaoHang = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCapNhap = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoHangs", x => x.ID_GiaoHang);
                    table.CheckConstraint("CK_GiaoHang_TrangThaiGiaoHang", "TrangThaiGiaoHang IN (N'Chưa phân công', N'Đang giao', N'Đã giao', N'Đang xử lý', N'Đang chờ giao hàng', N'Gặp sự cố', N'Bị hủy', N'Giao lại')");
                    table.ForeignKey(
                        name: "FK_GiaoHangs_DonHangs_ID_Don_Hang",
                        column: x => x.ID_Don_Hang,
                        principalTable: "DonHangs",
                        principalColumn: "ID_Don_Hang");
                    table.ForeignKey(
                        name: "FK_GiaoHangs_ThanhToans_ID_ThanhToan",
                        column: x => x.ID_ThanhToan,
                        principalTable: "ThanhToans",
                        principalColumn: "ID_ThanhToan",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_User_KhachHangs_KhachHangEmail_KhachHangHoTen",
                table: "User_KhachHangs",
                columns: new[] { "KhachHangEmail", "KhachHangHoTen" });

            migrationBuilder.CreateIndex(
                name: "IX_User_KhachHangs_UserName",
                table: "User_KhachHangs",
                column: "UserName");
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
        }
    }
}
