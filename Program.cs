using Microsoft.EntityFrameworkCore;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;
using AuthDemo.Areas.Admin.Services;
using AuthDemo.Models.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using DotNetEnv;
using AuthDemo.Models;
using CloudinaryDotNet;
using AuthDemo.Services.VnPay;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<GhnService>();

// Load .env
Env.Load();
// Load .env
Env.Load(); Env.Load();

// Đọc biến từ ENV// Đọc biến từ ENV
var cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
var apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
var apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");

if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
{
    throw new InvalidOperationException("Biến môi trường Cloudinary bị thiếu!");
}

var cloudinarySettings = new CloudinarySettings
{
    CloudName = cloudName,
    ApiKey = apiKey,
    ApiSecret = apiSecret
};


// Đăng ký Cloudinary
builder.Services.AddSingleton(new Cloudinary(
    new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret)
));
// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));
// Vnpay configuration
builder.Services.Configure<VNPayConfig>(builder.Configuration.GetSection("VnPay"));
// vnpay khách
builder.Services.AddScoped<IVnPayService, VnPayService>();
// Đăng ký DI cho SanPhamService
builder.Services.AddScoped<IGiayService, GiayService>();
builder.Services.AddScoped<IThuongHieuService, ThuongHieuService>();
builder.Services.AddScoped<IMauSacService, MauSacService>();
builder.Services.AddScoped<IKichThuocService, KichThuocService>();
builder.Services.AddScoped<IChatLieuService, ChatLieuService>();
builder.Services.AddScoped<IChiTietGiayService, ChiTietGiayService>();
builder.Services.AddScoped<IBanHangTaiQuayService, BanHangTaiQuayService>();
builder.Services.AddScoped<IHoaDonService, HoaDonService>();
builder.Services.AddScoped<IVNPayService, VNPayService>();
builder.Services.AddScoped<IDanhMucService, DanhMucService>();



builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 10485760; // Giới hạn 10MB cho upload file
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
