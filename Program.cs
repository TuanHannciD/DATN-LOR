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

//Add Cookie Policy
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    //cho phép JavaScript truy cập cookie (nếu cần)
    options.Cookie.HttpOnly = true;
    // Đảm bảo cookie được gửi qua HTTPS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    // SameSite policy
    // SameSiteMode.Lax: Cookie sẽ được gửi trong các yêu cầu cùng site và một số yêu cầu cross-site nhất định (như GET từ liên kết bên ngoài).
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=BanHangTaiQuay}/{action=Index}/{id?}")
    .RequireAuthorization("AdminOnly");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
