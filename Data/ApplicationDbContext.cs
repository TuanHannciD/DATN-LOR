using Microsoft.EntityFrameworkCore;
using AuthDemo.Models;

namespace AuthDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<User_KhachHang> User_KhachHangs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<ChatLieu> ChatLieus { get; set; }
        public DbSet<HangSX> HangSXs { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<MauSac> MauSacs { get; set; }
        public DbSet<SanPhamChiTiet> SanPhamChiTiets { get; set; }
        public DbSet<AnhSp> AnhSps { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<Gio_Hang> Gio_Hangs { get; set; }
        public DbSet<Gio_Hang_Chi_Tiet> Gio_Hang_Chi_Tiets { get; set; }
        public DbSet<SanPhamYeuThich> SanPhamYeuThiches { get; set; }
        public DbSet<SanPhamYeuThichChiTiet> SanPhamYeuThichChiTiets { get; set; }
        public DbSet<SanPham_Mua> SanPham_Muas { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<Don_Hang_Thanh_Toan> Don_Hang_Thanh_Toans { get; set; }
        public DbSet<HoTroKhachHang> HoTroKhachHangs { get; set; }
        public DbSet<SanPham_ThanhToan> SanPham_ThanhToans { get; set; }
        public DbSet<TonKho> TonKhos { get; set; }
        public DbSet<NCC> NCCs { get; set; }
        public DbSet<GiaoHang> GiaoHangs { get; set; }
        public DbSet<BaoCao> BaoCaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Username);

            // Cấu hình KhachHang với khóa chính tổng hợp
            modelBuilder.Entity<KhachHang>()
                .HasKey(kh => new { kh.Email, kh.HoTen });

            // Cấu hình KhachHang: UserName là khóa ngoại đến Users
            modelBuilder.Entity<KhachHang>()
                .HasOne(kh => kh.User)
                .WithMany()
                .HasForeignKey(kh => kh.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình User_KhachHang với khóa ngoại tổng hợp đến KhachHang
            modelBuilder.Entity<User_KhachHang>()
                .HasOne(uk => uk.User)
                .WithMany()
                .HasForeignKey(uk => uk.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình Admin: UserName là khóa ngoại đến Users
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình NhanVien: UserName là khóa ngoại đến Users
            modelBuilder.Entity<NhanVien>()
                .HasOne(nv => nv.User)
                .WithMany()
                .HasForeignKey(nv => nv.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình KhachHang: UserName là khóa ngoại đến Users
            modelBuilder.Entity<KhachHang>()
                .HasOne(kh => kh.User)
                .WithMany()
                .HasForeignKey(kh => kh.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình User_KhachHang: UserName là khóa ngoại đến Users
            modelBuilder.Entity<User_KhachHang>()
                .HasOne(uk => uk.User)
                .WithMany()
                .HasForeignKey(uk => uk.UserName)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.AdminConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.NhanVienConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.KhachHangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.User_KhachHangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.ChatLieuConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HangSXConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SizeConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.MauSacConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamChiTietConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.AnhSpConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.KhuyenMaiConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Gio_HangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Gio_Hang_Chi_TietConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamYeuThichConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamYeuThichChiTietConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPham_MuaConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HoaDonConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.ThanhToanConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.DonHangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Don_Hang_Thanh_ToanConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HoTroKhachHangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPham_ThanhToanConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.TonKhoConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.NCCConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.GiaoHangConfiguration());
            // modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.BaoCaoConfiguration());

            // Xóa các cấu hình User_KhachHang đã di chuyển sang User_KhachHangConfiguration
            // modelBuilder.Entity<User_KhachHang>()
            //     .HasOne(uk => uk.KhachHang)
            //     .WithMany()
            //     .HasForeignKey(uk => uk.Email);

            // modelBuilder.Entity<User_KhachHang>()
            //     .HasOne(uk => uk.User)
            //     .WithMany()
            //     .HasForeignKey(uk => uk.UserName)
            //     .OnDelete(DeleteBehavior.Restrict);

            // Xóa các ràng buộc CHECK của SanPhamChiTiet đã di chuyển sang SanPhamChiTietConfiguration
            // modelBuilder.Entity<SanPhamChiTiet>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_SanPhamChiTiet_SoLuongBan", "SoLuongBan >= 0"));
            // modelBuilder.Entity<SanPhamChiTiet>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_SanPhamChiTiet_Gia", "Gia > 0"));

            // Ví dụ: for KhuyenMai.GiaTriKm
            // modelBuilder.Entity<KhuyenMai>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_GiaTriKm", "GiaTriKm > 0"));
            // modelBuilder.Entity<KhuyenMai>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_LoaiKm", "LoaiKm IN (N'Phần trăm', N'Số tiền cố định', N'Miễn phí giao hàng')"));
            // modelBuilder.Entity<KhuyenMai>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_SoLuong", "SoLuong >= 0"));
            // modelBuilder.Entity<KhuyenMai>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_KhuyenMai_SoLuong1Ng", "SoLuong1Ng >= 0"));

            // Ví dụ: for BaoCao
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_LoaiBaoCao", "LoaiBaoCao IN (N'DoanhSo', N'HangTon', N'DonHang', N'KhachHang')"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_DoanhThu", "DoanhThu >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongHangBanRa", "SoLuongHangBanRa >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongHangTon", "SoLuongHangTon >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_TongSoDonHang", "TongSoDonHang >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangHoanThanh", "SoLuongDonHangHoanThanh >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangDangXuLy", "SoLuongDonHangDangXuLy >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoLuongDonHangBiHuy", "SoLuongDonHangBiHuy >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_TongSoKhachHang", "TongSoKhachHang >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoKhachHangMoi", "SoKhachHangMoi >= 0"));
            // modelBuilder.Entity<BaoCao>()
            //     .ToTable(tb => tb.HasCheckConstraint("CK_BaoCao_SoKhachHangTroLai", "SoKhachHangTroLai >= 0"));
        }
    }
} 