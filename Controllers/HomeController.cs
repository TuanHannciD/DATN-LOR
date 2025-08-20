using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AuthDemo.Models;
using Microsoft.AspNetCore.Http;

//using AuthDemo.Migrations;

using AuthDemo.Data;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Models.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;
using System.Collections.Generic;
using X.PagedList;

namespace AuthDemo.Controllers;
 
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    
    public IActionResult Index(int ? page)
    {
        int pagesize = 8;
        int pagenumber = page == null || page < 0 ? 1 : page.Value;

        var product = _context.Giays
            //.Where(g => g.TrangThai ==0 )
            .Where(g => g.ChiTietGiays.Any())
    .Select(g => new ProductViewModel
    {
        ShoeID = g.ShoeID,
        TenGiay = g.TenGiay,
        AnhDaiDien = g.AnhDaiDien,
        GiaThapNhat = g.ChiTietGiays.Min(ct => ct.Gia)
    })
        .ToList();


        PagedList<ProductViewModel> list = new PagedList<ProductViewModel>(product, pagenumber, pagesize);
        return View(list);
        

    }
    // hiển thị sản phẩm chi tiết
    public IActionResult SanPhamdetails(Guid id)
    {


        var sanPham = _context.Giays.Find(id);


        var chiTietList = _context.ChiTietGiays
            .Include(c => c.KichThuoc)
            .Include(c => c.MauSac)
            .Include(c => c.ThuongHieu)
            .Include(c => c.DanhMuc)
            .Include(c=>c.AnhGiays)
            .Where(c => c.ShoeID == id)
            .ToList();

         var allImagePaths = chiTietList
       .SelectMany(ct => ct.AnhGiays)
       .Select(img => img.DuongDanAnh)
       .ToList();


        var gia = chiTietList.Select(x => x.Gia).ToList();
        var gianhonhat = chiTietList.Min(x => x.Gia);      
        var mauSacList = chiTietList.Select(x => x.MauSac).Distinct().ToList();
        var kichCoList = chiTietList.Select(x => x.KichThuoc).Distinct().ToList();
        var thuongHieuList = chiTietList.Select(x => x.ThuongHieu).Distinct().ToList();
        var danhmuclist = chiTietList.Select(x => x.DanhMuc).ToList();

        var sanPhamLienQuan = _context.ChiTietGiays
     .Include(c => c.Giay)
     .Include(c => c.DanhMuc)
     .Where(c => danhmuclist.Contains(c.DanhMuc) && c.ShoeID != sanPham.ShoeID)
     .GroupBy(c => c.ShoeID)
     .Select(g => new SanPhamLqVM
     {
         Giay = g.First().Giay,
         GiaMin = g.Min(x => x.Gia)
     })
     .ToList();

        var model = new ProductDetailsVM
        {
            giay = sanPham,            
            MauSacOptions = mauSacList,
            KichCoOptions = kichCoList,
            ThuongHieuOptions = thuongHieuList,
            DanhSachAnh = allImagePaths,
            SanPhamLienQuan = sanPhamLienQuan
        };



        ViewBag.Gia = gianhonhat;
        return View(model);

    }

    [HttpGet]
    public JsonResult GetAvailableSizes(Guid shoeId, Guid colorId)
    {
        var validSizeIds = _context.ChiTietGiays
            .Where(c => c.ShoeID == shoeId && c.ColorID == colorId && c.SoLuong > 0)
            .Select(c => c.SizeID)
            .Distinct()
            .ToList();

        return Json(validSizeIds);
    }

    [HttpGet]
    public JsonResult GetAvailableColors(Guid shoeId, Guid sizeId)
    {
        var validColorIds = _context.ChiTietGiays
            .Where(c => c.ShoeID == shoeId && c.SizeID == sizeId && c.SoLuong > 0)
            .Select(c => c.ColorID)
            .Distinct()
            .ToList();

        return Json(validColorIds);
    }

    [HttpGet]
    public JsonResult GetSoLuongTon(Guid shoeId, Guid colorId, Guid sizeId)
    {
        var chiTiet = _context.ChiTietGiays.FirstOrDefault(c =>
            c.ShoeID == shoeId && c.ColorID == colorId && c.SizeID == sizeId);

        if (chiTiet != null)
        {
            return Json(new { success = true, soLuong = chiTiet.SoLuong });
        }

        return Json(new { success = false, soLuong = 0 });
    }

    [HttpGet]
    public JsonResult GetGiaChiTiet(Guid shoeId, Guid colorId, Guid sizeId)
    {
        var chiTiet = _context.ChiTietGiays.FirstOrDefault(c =>
            c.ShoeID == shoeId && c.ColorID == colorId && c.SizeID == sizeId);

        if (chiTiet != null)
        {
            return Json(new { success = true, gia = chiTiet.Gia });
        }

        return Json(new { success = false, gia = 0 });
    }
   

    [HttpPost]
    public JsonResult Cart(Guid id, Guid MauSacId, Guid KichCoId, int quantity)
    {
        // Bước 1: Lấy thông tin UserID (tạm gọi là CartID — thường là theo phiên đăng nhập)
        /*Guid cartId = GetCurrentUserId();*/ // bạn có thể thay bằng HttpContext.User.Identity nếu dùng Identity

        var check = HttpContext.Session.GetString("TenDangNhap");

        if (string.IsNullOrEmpty(check))
        {
            var loginUrl = Url.Action("Login", "Account");
            return Json(new { success = false, redirect = loginUrl });
        }
        //lấy iduser
        var users = _context.NguoiDungs.FirstOrDefault(a => a.TenDangNhap == check).UserID;

        //lấy giỏ hàng theo id user
        var idcart = _context.GioHangs.FirstOrDefault(a => a.UserID == users).CartID;

        // Bước 2: Tìm ChiTietGiay tương ứng với sản phẩm
        var shoeDetail = _context.ChiTietGiays.FirstOrDefault(x =>
            x.ShoeID == id && x.ColorID == MauSacId && x.SizeID == KichCoId);
        
        var cartItem = _context.ChiTietGioHangs.FirstOrDefault(p => p.CartID == idcart && p.ShoeDetailID == shoeDetail.ShoeDetailID);

        var result = new { success = false, message = "" };
        
        if (shoeDetail == null)
        {          
            result = new { success = false, message = "Chọn size và màu phù hợp" };
        }
        if (quantity >0)
        {
            if (quantity <= shoeDetail.SoLuong)
            {
                if (cartItem != null)
                {
                    cartItem.SoLuong += quantity;
                    //db.SanPhams.Update(product);
                    _context.ChiTietGioHangs.Update(cartItem);
                    _context.SaveChanges();
                    result = new { success = true, message = "Thêm thành công!" };
                }
                else
                {
                    // Bước 3: Tạo bản ghi CartDetail
                    var cartDetail = new ChiTietGioHang
                    {
                        CartDetailID = Guid.NewGuid(),
                        CartID = idcart,
                        ShoeDetailID = shoeDetail.ShoeDetailID,
                        SoLuong = quantity,
                        KichThuoc = _context.KichThuocs.FirstOrDefault(x => x.SizeID == KichCoId)?.TenKichThuoc ?? "N/A",
                        NguoiTao = "system",
                        NguoiCapNhat = "system",
                        NgayTao = DateTime.MinValue,
                        NgayCapNhat = DateTime.MinValue
                    };

                    _context.ChiTietGioHangs.Add(cartDetail);
                    _context.SaveChanges();
                    result = new { success = true, message = "Thêm thành công!" };
                }

            }
            else
            {
                result = new { success = false, message = "Số Lượng không đủ!" };
            }
        }
        else if(quantity == null)
        {
            result = new { success = false, message = "vui lòng nhập số lượng!" };
        }
        else if (quantity <= 0)
        {
            result = new { success = false, message = "số lượng phải lớn hơn 0!" };
        }    
       
        return Json(result);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
