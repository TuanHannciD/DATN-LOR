using AuthDemo.Data;
using AuthDemo.Models;
using AuthDemo.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VoucherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VoucherController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Admin/Voucher
        public async Task<IActionResult> Index(string searchString)
        {
            var vouchers = from v in _db.Vouchers
                           where v.IsDelete == false
                           select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                vouchers = vouchers.Where(v => v.MaVoucherCode.Contains(searchString) || v.MoTa.Contains(searchString));
            }

            return View(await vouchers.ToListAsync());
        }

        // GET: Admin/Voucher/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Voucher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vouchers voucher)
        {
            // Validate ngày
            if (voucher.NgayBatDau == default || voucher.NgayKetThuc == default)
            {
                ModelState.AddModelError("", "Ngày bắt đầu và ngày kết thúc là bắt buộc.");
            }
            else if (voucher.NgayKetThuc <= voucher.NgayBatDau)
            {
                ModelState.AddModelError("NgayKetThuc", "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }
            else if (voucher.NgayBatDau < DateTime.Now.Date)
            {
                ModelState.AddModelError("NgayBatDau", "Ngày bắt đầu không được nhỏ hơn ngày hiện tại.");
            }

            // Validate giá trị
            if (voucher.GiaTri <= 0)
            {
                ModelState.AddModelError("GiaTri", "Giá trị giảm phải lớn hơn 0.");
            }

            if (voucher.LoaiGiam == LoaiGiam.PhanTram)
            {
                if (voucher.GiaTri > 100)
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm phần trăm không được vượt quá 100%.");
                }
                if (!Regex.IsMatch(voucher.GiaTri.ToString(), @"^\d+(\.\d{1,2})?$"))
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm phần trăm chỉ được nhập số (tối đa 2 chữ số thập phân).");
                }
                
            }
            else if (voucher.LoaiGiam == LoaiGiam.Tien)
            {
                if (!Regex.IsMatch(voucher.GiaTri.ToString(), @"^\d+(\.\d+)?$"))
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm tiền chỉ được nhập số.");
                }
            }
            // Validate GiaTriToiDa
            if (voucher.LoaiGiam == LoaiGiam.PhanTram)
            {
                if (voucher.GiaTriToiDa.HasValue && voucher.GiaTriToiDa <= 0)
                {
                    ModelState.AddModelError("GiaTriToiDa", "Giá trị tối đa phải lớn hơn 0 khi giảm theo phần trăm.");
                }
            }
            else if (voucher.LoaiGiam == LoaiGiam.Tien)
            {
                if (voucher.GiaTriToiDa.HasValue)
                {
                    ModelState.AddModelError("GiaTriToiDa", "Giá trị tối đa không được nhập khi giảm theo tiền.");
                    // Loại bỏ GiaTriToiDa khỏi model để tránh gửi dữ liệu không mong muốn
                    ModelState.Remove("GiaTriToiDa");
                    voucher.GiaTriToiDa = null;
                }
            }

            // Validate số lần sử dụng
            if (voucher.SoLanSuDung <= 0 || voucher.SoLanSuDung == null)
            {
                ModelState.AddModelError("SoLanSuDung", "Số lượt sử dụng phải lớn hơn 0.");
            }

            // Validate tên voucher
            if (string.IsNullOrWhiteSpace(voucher.MaVoucherCode))
            {
                ModelState.AddModelError("MaVoucherCode", "Tên voucher là bắt buộc.");
            }
            else if (_db.Vouchers.Any(v => v.MaVoucherCode == voucher.MaVoucherCode && v.IsDelete == false))
            {
                ModelState.AddModelError("MaVoucherCode", "Tên voucher đã tồn tại.");
            }
           

            if (!ModelState.IsValid)
            {
                return View(voucher);
            }

            voucher.VoucherID = Guid.NewGuid();
            voucher.NgayTao = DateTime.Now;
            voucher.NgayCapNhat = DateTime.Now;
            voucher.TrangThai = true; // Mặc định là active khi tạo mới

            _db.Add(voucher);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thêm voucher thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Voucher/Edit/id
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _db.Vouchers.FindAsync(id);
            if (voucher == null )
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Admin/Voucher/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Vouchers voucher)
        {
            if (id != voucher.VoucherID)
            {
                return NotFound();
            }

            // Validate ngày
            if (voucher.NgayBatDau == default || voucher.NgayKetThuc == default)
            {
                ModelState.AddModelError("", "Ngày bắt đầu và ngày kết thúc là bắt buộc.");
            }
            else if (voucher.NgayKetThuc <= voucher.NgayBatDau)
            {
                ModelState.AddModelError("NgayKetThuc", "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }
            else if (voucher.NgayBatDau < DateTime.Now.Date)
            {
                ModelState.AddModelError("NgayBatDau", "Ngày bắt đầu không được nhỏ hơn ngày hiện tại.");
            }

            // Validate tên voucher (trừ voucher hiện tại)
            if (string.IsNullOrWhiteSpace(voucher.MaVoucherCode))
            {
                ModelState.AddModelError("MaVoucherCode", "Tên voucher là bắt buộc.");
            }
            else if (_db.Vouchers.Any(v => v.VoucherID != id && v.MaVoucherCode == voucher.MaVoucherCode && v.IsDelete == false))
            {
                ModelState.AddModelError("MaVoucherCode", "Tên voucher đã tồn tại.");
            }
            // Validate giá trị
            if (voucher.GiaTri <= 0)
            {
                ModelState.AddModelError("GiaTri", "Giá trị giảm phải lớn hơn 0.");
            }

            if (voucher.LoaiGiam == LoaiGiam.PhanTram)
            {
                if (voucher.GiaTri > 100)
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm phần trăm không được vượt quá 100%.");
                }
                if (!Regex.IsMatch(voucher.GiaTri.ToString(), @"^\d+(\.\d{1,2})?$"))
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm phần trăm chỉ được nhập số (tối đa 2 chữ số thập phân).");
                }
               
            }
            else if (voucher.LoaiGiam == LoaiGiam.Tien)
            {
                if (!Regex.IsMatch(voucher.GiaTri.ToString(), @"^\d+(\.\d+)?$"))
                {
                    ModelState.AddModelError("GiaTri", "Giá trị giảm tiền chỉ được nhập số.");
                }
            }
            // Validate GiaTriToiDa
            if (voucher.LoaiGiam == LoaiGiam.PhanTram)
            {
                if (voucher.GiaTriToiDa.HasValue && voucher.GiaTriToiDa <= 0)
                {
                    ModelState.AddModelError("GiaTriToiDa", "Giá trị tối đa phải lớn hơn 0 khi giảm theo phần trăm.");
                }
            }
            else if (voucher.LoaiGiam == LoaiGiam.Tien)
            {
                if (voucher.GiaTriToiDa.HasValue)
                {
                    ModelState.AddModelError("GiaTriToiDa", "Giá trị tối đa không được nhập khi giảm theo tiền.");
                    // Loại bỏ GiaTriToiDa khỏi model để tránh gửi dữ liệu không mong muốn
                    ModelState.Remove("GiaTriToiDa");
                    voucher.GiaTriToiDa = null;
                }
            }
            // Validate số lần sử dụng
            if (voucher.SoLanSuDung <= 0 || voucher.SoLanSuDung == null)
            {
                ModelState.AddModelError("SoLanSuDung", "Số lượt sử dụng phải lớn hơn 0.");
            }


            if (!ModelState.IsValid)
            {
                return View(voucher);
            }

            try
            {
                var existingVoucher = await _db.Vouchers.FindAsync(id);
                if (existingVoucher == null)
                {
                    return NotFound();
                }

                // Cập nhật các thuộc tính
                existingVoucher.MaVoucherCode = voucher.MaVoucherCode;
                existingVoucher.MoTa = voucher.MoTa;
                existingVoucher.LoaiGiam = voucher.LoaiGiam;
                existingVoucher.GiaTri = voucher.GiaTri;
                existingVoucher.GiaTriToiDa = voucher.GiaTriToiDa;
                existingVoucher.DonHangToiThieu = voucher.DonHangToiThieu;
                existingVoucher.SoLanSuDung = voucher.SoLanSuDung;
                existingVoucher.SoLanSuDungMoiUser = voucher.SoLanSuDungMoiUser;
                existingVoucher.NgayBatDau = voucher.NgayBatDau;
                existingVoucher.NgayKetThuc = voucher.NgayKetThuc;
                existingVoucher.NgayCapNhat = DateTime.Now;

                _db.Update(existingVoucher);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật voucher thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherExists(voucher.VoucherID))
                {
                    return NotFound();
                }
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(Guid id)
        {
            return _db.Vouchers.Any(e => e.VoucherID == id && e.IsDelete == false);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(string id, bool trangThai)
        {
            try
            {
                Console.WriteLine($"Received ID: {id}, TrangThai: {trangThai}");

                // Parse ID từ string sang Guid
                if (!Guid.TryParse(id, out Guid voucherId))
                {
                    Console.WriteLine($"Invalid GUID format: {id}");
                    return Json(new { success = false, message = "ID voucher không hợp lệ." });
                }

                var voucher = await _db.Vouchers.FindAsync(voucherId);
                if (voucher == null || voucher.IsDelete)
                {
                    Console.WriteLine($"Voucher not found or deleted: {voucherId}");
                    return Json(new { success = false, message = "Không tìm thấy voucher." });
                }

                voucher.TrangThai = trangThai;
                voucher.NgayCapNhat = DateTime.Now;

                _db.Update(voucher);
                await _db.SaveChangesAsync();

                Console.WriteLine($"Successfully updated Voucher ID: {voucherId}, New TrangThai: {trangThai}");
                return Json(new { success = true, message = "Cập nhật trạng thái thành công!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return Json(new { success = false, message = $"Lỗi: {ex.Message}" });
            }
        }
    }
}