using Microsoft.AspNetCore.Mvc;
using AuthDemo.Data;
using AuthDemo.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ThongKeController(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task<IActionResult>
    ThongKe()
        {
            try
            {
                // Fetch order status statistics
                var thongKe = await _db.HoaDons
                .GroupBy(h => h.TrangThai)
                .Select(g => new
                {
                    TrangThai = g.Key.ToString(),
                    SoLuong = g.Count()
                })
                .ToListAsync();

                // Prepare data for the view
                var labels = thongKe.Select(t => t.TrangThai).ToArray();
                var data = thongKe.Select(t => t.SoLuong).ToArray();

                ViewBag.Labels = labels;
                ViewBag.Data = data;

                return View();
            }
            catch (Exception ex)
            {
                // Log lỗi và trả về view bình thường
                Console.WriteLine($"Error in ThongKe: {ex.Message}");
                ViewBag.Labels = new string[] { "Không có dữ liệu" };
                ViewBag.Data = new int[] { 0 };
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult>
            GetStatistics(string filterType, string customDate = null)
        {
            try
            {
                DateTime startDate, endDate = DateTime.Now;

                switch (filterType?.ToLower())
                {
                    case "today":
                        startDate = DateTime.Today;
                        endDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                        break;
                    case "week":
                        startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                        endDate = startDate.AddDays(7).AddSeconds(-1);
                        break;
                    case "month":
                        startDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                        endDate = startDate.AddMonths(1).AddSeconds(-1);
                        break;
                    case "year":
                        startDate = new DateTime(DateTime.Today.Year, 1, 1);
                        endDate = startDate.AddYears(1).AddSeconds(-1);
                        break;
                    case "custom":
                        if (DateTime.TryParse(customDate, out var customStartDate))
                        {
                            startDate = customStartDate.Date;
                            endDate = startDate.AddDays(1).AddSeconds(-1);
                        }
                        else
                        {
                            startDate = DateTime.Today.AddMonths(-1);
                            endDate = DateTime.Now;
                        }
                        break;
                    default:
                        startDate = DateTime.MinValue;
                        endDate = DateTime.MaxValue;
                        break;
                }

                // Lấy dữ liệu thực từ database
                var totalRevenue = await CalculateTotalRevenue(startDate, endDate);
                var totalProductsSold = await CalculateTotalProductsSold(startDate, endDate);
                var totalOrders = await CalculateTotalOrders(startDate, endDate);
                var orderStatusStats = await GetOrderStatusStats(startDate, endDate);
                var salesData = await GetSalesData(startDate, endDate, filterType);

                return Ok(new
                {
                    totalRevenue = totalRevenue,
                    totalProductsSold = totalProductsSold,
                    totalOrders = totalOrders,
                    orderStatusStats = new
                    {
                        labels = orderStatusStats.Keys.ToArray(),
                        data = orderStatusStats.Values.ToArray()
                    },
                    salesData = new
                    {
                        labels = salesData.Keys.ToArray(),
                        data = salesData.Values.ToArray()
                    }
                });
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                Console.WriteLine($"Error in GetStatistics: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Trả về dữ liệu mẫu để tránh lỗi frontend
                return Ok(new
                {
                    totalRevenue = 0,
                    totalProductsSold = 0,
                    totalOrders = 0,
                    orderStatusStats = new
                    {
                        labels = new string[] { "Không có dữ liệu" },
                        data = new int[] { 0 }
                    },
                    salesData = new
                    {
                        labels = new string[] { "Không có dữ liệu" },
                        data = new decimal[] { 0 }
                    }
                });
            }
        }

        private async Task<decimal>
            CalculateTotalRevenue(DateTime startDate, DateTime endDate)
        {
            try
            {
                // CHỈ tính doanh thu từ các đơn hàng ĐÃ GIAO
                var total = await _db.ChiTietHoaDons
    .Where(ct => ct.HoaDon.TrangThai == TrangThaiHoaDon.DaGiao &&
                 ct.HoaDon.NgayTao >= startDate &&
                 ct.HoaDon.NgayTao <= endDate)
    .SumAsync(ct => (decimal?)(ct.SoLuong * ct.DonGia)) ?? 0;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private async Task<int>
            CalculateTotalProductsSold(DateTime startDate, DateTime endDate)
        {
            try
            {
                // CHỈ tính sản phẩm từ các đơn hàng ĐÃ GIAO
                var total = await _db.ChiTietHoaDons
                .Include(ct => ct.HoaDon)
                .Where(ct => ct.HoaDon.TrangThai == TrangThaiHoaDon.DaGiao && // Chỉ đơn hàng đã giao
                ct.HoaDon.NgayTao >= startDate &&
                ct.HoaDon.NgayTao <= endDate)
                .SumAsync(ct => (int?)ct.SoLuong) ?? 0;

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private async Task<int>
            CalculateTotalOrders(DateTime startDate, DateTime endDate)
        {
            try
            {
                // Vẫn tính tổng số đơn hàng (tất cả trạng thái)
                var total = await _db.HoaDons
                .Where(h => h.NgayTao >= startDate &&
                h.NgayTao <= endDate)
                .CountAsync();

                return total;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private async Task<Dictionary<string, int>> GetOrderStatusStats(DateTime startDate, DateTime endDate)
        {
            try
            {
                var stats = await _db.HoaDons
                    .Where(h => h.NgayTao >= startDate &&
                               h.NgayTao <= endDate)
                    .GroupBy(h => h.TrangThai)
                    .Select(g => new
                    {
                        TrangThai = g.Key.ToString(),
                        SoLuong = g.Count()
                    })
                    .ToListAsync();

                var result = new Dictionary<string, int>();
                foreach (var item in stats)
                {
                    result[item.TrangThai] = item.SoLuong;
                }

                // Đảm bảo có tất cả các trạng thái
                var allStatuses = Enum.GetValues(typeof(TrangThaiHoaDon)).Cast<TrangThaiHoaDon>();
                foreach (var status in allStatuses)
                {
                    var statusStr = status.ToString();
                    if (!result.ContainsKey(statusStr))
                    {
                        result[statusStr] = 0;
                    }
                }

                return result;
            }
            catch (Exception)
            {
                // Trả về dictionary với giá trị mặc định
                var result = new Dictionary<string, int>();
                var allStatuses = Enum.GetValues(typeof(TrangThaiHoaDon)).Cast<TrangThaiHoaDon>();
                foreach (var status in allStatuses)
                {
                    result[status.ToString()] = 0;
                }
                return result;
            }
        }

        private async Task<Dictionary<string, decimal>> GetSalesData(DateTime startDate, DateTime endDate, string filterType)
        {
            try
            {
                var salesData = new Dictionary<string, decimal>();

                if (filterType?.ToLower() == "today")
                {
                    // Doanh thu theo giờ - CHỈ đơn hàng ĐÃ GIAO
                    var hourlySales = await _db.HoaDons
                        .Where(h => h.TrangThai == TrangThaiHoaDon.DaGiao && // Chỉ đơn hàng đã giao
                                   h.NgayTao != null &&
                                   h.NgayTao >= startDate &&
                                   h.NgayTao <= endDate)
                        .GroupBy(h => h.NgayTao.Value.Hour)
                        .Select(g => new
                        {
                            Gio = g.Key,
                            DoanhThu = g.Sum(h => (decimal?)h.TongTien) ?? 0
                        })
                        .OrderBy(x => x.Gio)
                        .ToListAsync();

                    for (int hour = 0; hour < 24; hour++)
                    {
                        var hourData = hourlySales.FirstOrDefault(x => x.Gio == hour);
                        salesData[$"{hour}:00"] = hourData?.DoanhThu ?? 0;
                    }
                }
                else if (filterType?.ToLower() == "week" || filterType?.ToLower() == "month")
                {
                    var dailySales = await _db.HoaDons
                        .Where(h => h.TrangThai == TrangThaiHoaDon.DaGiao && // Chỉ đơn hàng đã giao
                                   h.NgayTao != null &&
                                   h.NgayTao >= startDate &&
                                   h.NgayTao <= endDate)
                        .GroupBy(h => h.NgayTao.Value.Date)
                        .Select(g => new
                        {
                            Ngay = g.Key,
                            DoanhThu = g.Sum(h => (decimal?)h.TongTien) ?? 0
                        })
                        .OrderBy(x => x.Ngay)
                        .ToListAsync();

                    if (filterType?.ToLower() == "week")
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            var currentDate = startDate.AddDays(i);
                            var dayData = dailySales.FirstOrDefault(x => x.Ngay.Date == currentDate.Date);
                            salesData[currentDate.ToString("dd/MM")] = dayData?.DoanhThu ?? 0;
                        }
                    }
                    else
                    {
                        int daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
                        for (int i = 0; i < daysInMonth; i++)
                        {
                            var currentDate = startDate.AddDays(i);
                            var dayData = dailySales.FirstOrDefault(x => x.Ngay.Date == currentDate.Date);
                            salesData[currentDate.ToString("dd/MM")] = dayData?.DoanhThu ?? 0;
                        }
                    }
                }
                else if (filterType?.ToLower() == "custom")
                {
                    var dailySales = await _db.HoaDons
                        .Where(h => h.TrangThai == TrangThaiHoaDon.DaGiao && // Chỉ đơn hàng đã giao
                                   h.NgayTao != null &&
                                   h.NgayTao >= startDate &&
                                   h.NgayTao <= endDate)
                        .GroupBy(h => h.NgayTao.Value.Date)
                        .Select(g => new
                        {
                            Ngay = g.Key,
                            DoanhThu = g.Sum(h => (decimal?)h.TongTien) ?? 0
                        })
                        .OrderBy(x => x.Ngay)
                        .ToListAsync();

                    var currentDate = startDate;
                    var dayData = dailySales.FirstOrDefault(x => x.Ngay.Date == currentDate.Date);
                    salesData[currentDate.ToString("dd/MM/yyyy")] = dayData?.DoanhThu ?? 0;
                }
                else
                {
                    var monthlySales = await _db.HoaDons
                        .Where(h => h.TrangThai == TrangThaiHoaDon.DaGiao && // Chỉ đơn hàng đã giao
                                   h.NgayTao != null &&
                                   h.NgayTao >= startDate &&
                                   h.NgayTao <= endDate)
                        .GroupBy(h => new { h.NgayTao.Value.Year, h.NgayTao.Value.Month })
                        .Select(g => new
                        {
                            Thang = $"{g.Key.Month}/{g.Key.Year}",
                            DoanhThu = g.Sum(h => (decimal?)h.TongTien) ?? 0
                        })
                        .OrderBy(x => x.Thang)
                        .ToListAsync();

                    foreach (var item in monthlySales)
                    {
                        salesData[item.Thang] = item.DoanhThu;
                    }
                }

                return salesData;
            }
            catch (Exception)
            {
                return new Dictionary<string, decimal> { { "Không có dữ liệu", 0 } };
            }
        }
    }
}
