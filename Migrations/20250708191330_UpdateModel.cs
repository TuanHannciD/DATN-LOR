using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatLieus",
                columns: table => new
                {
                    MaterialID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenChatLieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaChatLieuCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatLieus", x => x.MaterialID);
                });

            migrationBuilder.CreateTable(
                name: "DanhMucs",
                columns: table => new
                {
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDanhMuc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaDanhMucCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ParentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhMucs", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Giays",
                columns: table => new
                {
                    ShoeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenGiay = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaGiayCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giays", x => x.ShoeID);
                });

            migrationBuilder.CreateTable(
                name: "KichThuocs",
                columns: table => new
                {
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKichThuoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaKichThuocCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KichThuocs", x => x.SizeID);
                });

            migrationBuilder.CreateTable(
                name: "MauSacs",
                columns: table => new
                {
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaMau = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSacs", x => x.ColorID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnhDaiDien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Token = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayHetHanToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieus",
                columns: table => new
                {
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaThuongHieuCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieus", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "VaiTros",
                columns: table => new
                {
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenVaiTro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTros", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "DiaChis",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiaChiDayDu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaChis", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_DiaChi_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangs",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangs", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_GioHang_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DaThanhToan = table.Column<bool>(type: "bit", nullable: false),
                    PhuongThucThanhToan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DaHuy = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_HoaDon_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGiays",
                columns: table => new
                {
                    ShoeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGiays", x => x.ShoeDetailID);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_ChatLieu",
                        column: x => x.MaterialID,
                        principalTable: "ChatLieus",
                        principalColumn: "MaterialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_DanhMuc",
                        column: x => x.CategoryID,
                        principalTable: "DanhMucs",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_Giay",
                        column: x => x.ShoeID,
                        principalTable: "Giays",
                        principalColumn: "ShoeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_KichThuoc",
                        column: x => x.SizeID,
                        principalTable: "KichThuocs",
                        principalColumn: "SizeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_MauSac",
                        column: x => x.ColorID,
                        principalTable: "MauSacs",
                        principalColumn: "ColorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietGiay_ThuongHieu",
                        column: x => x.BrandID,
                        principalTable: "ThuongHieus",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VaiTroNguoiDungs",
                columns: table => new
                {
                    UserRoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaiTroNguoiDungs", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDung_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaiTroNguoiDung_VaiTro",
                        column: x => x.RoleID,
                        principalTable: "VaiTros",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuHoaDons",
                columns: table => new
                {
                    BillHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChiCu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DiaChiMoi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TrangThaiCu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThaiMoi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuHoaDons", x => x.BillHistoryID);
                    table.ForeignKey(
                        name: "FK_LichSuHoaDon_HoaDon",
                        column: x => x.BillID,
                        principalTable: "HoaDons",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LichSuHoaDon_NguoiDung",
                        column: x => x.UserID,
                        principalTable: "NguoiDungs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnhGiays",
                columns: table => new
                {
                    ImageShoeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DuongDanAnh = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnhGiays", x => x.ImageShoeID);
                    table.ForeignKey(
                        name: "FK_AnhGiay_ChiTietGiay",
                        column: x => x.ShoeDetailID,
                        principalTable: "ChiTietGiays",
                        principalColumn: "ShoeDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietGioHangs",
                columns: table => new
                {
                    CartDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    KichThuoc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietGioHangs", x => x.CartDetailID);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_ChiTietGiay",
                        column: x => x.ShoeDetailID,
                        principalTable: "ChiTietGiays",
                        principalColumn: "ShoeDetailID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietGioHang_GioHang",
                        column: x => x.CartID,
                        principalTable: "GioHangs",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    BillDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoeDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguoiCapNhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgayCapNhat = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.BillDetailID);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_ChiTietGiay",
                        column: x => x.ShoeDetailID,
                        principalTable: "ChiTietGiays",
                        principalColumn: "ShoeDetailID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDon_HoaDon",
                        column: x => x.BillID,
                        principalTable: "HoaDons",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ChatLieus",
                columns: new[] { "MaterialID", "MaChatLieuCode", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenChatLieu" },
                values: new object[,]
                {
                    { new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), "CTN", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Cotton" },
                    { new Guid("8a9b0c1d-2e3f-4a5b-6c7d-8e9f0a1b2c3d"), "LEN", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Len" }
                });

            migrationBuilder.InsertData(
                table: "DanhMucs",
                columns: new[] { "CategoryID", "MaDanhMucCode", "MoTa", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "ParentID", "TenDanhMuc" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), "SPORT", "Sản phẩm thể thao", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", null, "Thể thao" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "FASHION", "Sản phẩm thời trang", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", null, "Thời trang" }
                });

            migrationBuilder.InsertData(
                table: "Giays",
                columns: new[] { "ShoeID", "MaGiayCode", "MoTa", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenGiay", "TrangThai" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999999"), "AMX", "Giày thể thao Nike", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Air Max", "Còn hàng" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "UBS", "Giày thể thao Adidas", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Ultraboost", "Còn hàng" }
                });

            migrationBuilder.InsertData(
                table: "KichThuocs",
                columns: new[] { "SizeID", "MaKichThuocCode", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenKichThuoc" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "S", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "S" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "M", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "M" }
                });

            migrationBuilder.InsertData(
                table: "MauSacs",
                columns: new[] { "ColorID", "MaMau", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenMau" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "RED", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Đỏ" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "BLUE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Xanh" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "UserID", "AnhDaiDien", "Email", "HoTen", "IsActive", "MatKhau", "NgayCapNhat", "NgayHetHanToken", "NgayTao", "NguoiCapNhat", "NguoiTao", "SoDienThoai", "TenDangNhap", "Token" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "admin.jpg", "admin@example.com", "Quản trị viên", true, "OMadiOjAeYhAtMTjppvsDgOzcynJf9uc8ZC8/+0i1L8=", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "0123456789", "admin", "1234567890" });

            migrationBuilder.InsertData(
                table: "ThuongHieus",
                columns: new[] { "BrandID", "MaThuongHieuCode", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenThuongHieu" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555555"), "NIKE", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Nike" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "ADIDAS", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Adidas" }
                });

            migrationBuilder.InsertData(
                table: "VaiTros",
                columns: new[] { "RoleID", "MoTa", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TenVaiTro" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Quản trị viên", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Người dùng thường", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "user" }
                });

            migrationBuilder.InsertData(
                table: "ChiTietGiays",
                columns: new[] { "ShoeDetailID", "BrandID", "CategoryID", "ColorID", "Gia", "MaterialID", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "ShoeID", "SizeID", "SoLuong" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("77777777-7777-7777-7777-777777777777"), new Guid("11111111-1111-1111-1111-111111111111"), 2500000m, new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("99999999-9999-9999-9999-999999999999"), new Guid("33333333-3333-3333-3333-333333333333"), 10 });

            migrationBuilder.InsertData(
                table: "DiaChis",
                columns: new[] { "AddressID", "DiaChiDayDu", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "UserID" },
                values: new object[] { new Guid("44445555-6666-7777-8888-9999aaaabbbb"), "123 Đường ABC, Quận 1, TP.HCM", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "GioHangs",
                columns: new[] { "CartID", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "UserID" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "HoaDons",
                columns: new[] { "BillID", "DaHuy", "DaThanhToan", "DiaChi", "Email", "GhiChu", "HoTen", "LyDo", "NgayCapNhat", "NgayGiaoHang", "NgayTao", "NguoiCapNhat", "NguoiTao", "PhuongThucThanhToan", "SoDienThoai", "TongTien", "TrangThai", "UserID" },
                values: new object[] { new Guid("11112222-3333-4444-5555-666677778888"), false, true, "Hà Nội", "admin@example.com", "", "Nguyễn Văn A", "", null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Tiền mặt", "0123456789", 5000000m, "Đã thanh toán", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "VaiTroNguoiDungs",
                columns: new[] { "UserRoleID", "RoleID", "UserID" },
                values: new object[] { new Guid("aaaa1111-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "AnhGiays",
                columns: new[] { "ImageShoeID", "DuongDanAnh", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "ShoeDetailID" },
                values: new object[,]
                {
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "/images/airmax1.jpg", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "/images/airmax2.jpg", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }
                });

            migrationBuilder.InsertData(
                table: "ChiTietGioHangs",
                columns: new[] { "CartDetailID", "CartID", "KichThuoc", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "ShoeDetailID", "SoLuong" },
                values: new object[] { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "M", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2 });

            migrationBuilder.InsertData(
                table: "ChiTietHoaDons",
                columns: new[] { "BillDetailID", "BillID", "DonGia", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "ShoeDetailID", "SoLuong" },
                values: new object[] { new Guid("22223333-4444-5555-6666-777788889999"), new Guid("11112222-3333-4444-5555-666677778888"), 2500000m, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 1 });

            migrationBuilder.InsertData(
                table: "LichSuHoaDons",
                columns: new[] { "BillHistoryID", "BillCode", "BillID", "DiaChiCu", "DiaChiMoi", "GhiChu", "HoTen", "NgayCapNhat", "NgayTao", "NguoiCapNhat", "NguoiTao", "TrangThaiCu", "TrangThaiMoi", "UserID" },
                values: new object[] { new Guid("33334444-5555-6666-7777-88889999aaaa"), "HD001", new Guid("11112222-3333-4444-5555-666677778888"), "Hà Nội", "Hồ Chí Minh", "Chuyển địa chỉ giao hàng", "Nguyễn Văn A", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "system", "system", "Chờ xác nhận", "Đã thanh toán", new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.CreateIndex(
                name: "IX_AnhGiays_ShoeDetailID",
                table: "AnhGiays",
                column: "ShoeDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_BrandID",
                table: "ChiTietGiays",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_CategoryID",
                table: "ChiTietGiays",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_ColorID",
                table: "ChiTietGiays",
                column: "ColorID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_MaterialID",
                table: "ChiTietGiays",
                column: "MaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_ShoeID",
                table: "ChiTietGiays",
                column: "ShoeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGiays_SizeID",
                table: "ChiTietGiays",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_CartID",
                table: "ChiTietGioHangs",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietGioHangs_ShoeDetailID",
                table: "ChiTietGioHangs",
                column: "ShoeDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_BillID",
                table: "ChiTietHoaDons",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_ShoeDetailID",
                table: "ChiTietHoaDons",
                column: "ShoeDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_DiaChis_UserID",
                table: "DiaChis",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangs_UserID",
                table: "GioHangs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_UserID",
                table: "HoaDons",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuHoaDons_BillID",
                table: "LichSuHoaDons",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuHoaDons_UserID",
                table: "LichSuHoaDons",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_RoleID",
                table: "VaiTroNguoiDungs",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_VaiTroNguoiDungs_UserID",
                table: "VaiTroNguoiDungs",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhGiays");

            migrationBuilder.DropTable(
                name: "ChiTietGioHangs");

            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "DiaChis");

            migrationBuilder.DropTable(
                name: "LichSuHoaDons");

            migrationBuilder.DropTable(
                name: "VaiTroNguoiDungs");

            migrationBuilder.DropTable(
                name: "GioHangs");

            migrationBuilder.DropTable(
                name: "ChiTietGiays");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "VaiTros");

            migrationBuilder.DropTable(
                name: "ChatLieus");

            migrationBuilder.DropTable(
                name: "DanhMucs");

            migrationBuilder.DropTable(
                name: "Giays");

            migrationBuilder.DropTable(
                name: "KichThuocs");

            migrationBuilder.DropTable(
                name: "MauSacs");

            migrationBuilder.DropTable(
                name: "ThuongHieus");

            migrationBuilder.DropTable(
                name: "NguoiDungs");
        }
    }
}
