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
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.AdminConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.KhuyenMaiConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HoaDonConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.ThanhToanConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.NhanVienConfiguration());

           

            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.KhachHangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.User_KhachHangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.ChatLieuConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HangSXConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.MauSacConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SizeConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamChiTietConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.AnhSpConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Gio_HangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Gio_Hang_Chi_TietConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamYeuThichConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPhamYeuThichChiTietConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPham_MuaConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.DonHangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.Don_Hang_Thanh_ToanConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.HoTroKhachHangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.SanPham_ThanhToanConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.TonKhoConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.NCCConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.GiaoHangConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.BaoCaoConfiguration());
            modelBuilder.ApplyConfiguration(new AuthDemo.Models.Configurations.RoleConfiguration());
        }
    }
} 