using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class SanPhamChiTietService : ISanPhamChiTietService
    {
        private readonly ApplicationDbContext _db;
        public SanPhamChiTietService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<SanPhamChiTiet> GetAll() => _db.SanPhamChiTiets.ToList();
        public SanPhamChiTiet GetById(Guid id) => _db.SanPhamChiTiets.Find(id);
        public void Add(SanPhamChiTiet entity)
        {
            _db.SanPhamChiTiets.Add(entity);
            _db.SaveChanges();
        }
        public void Update(SanPhamChiTiet entity)
        {
            _db.SanPhamChiTiets.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var obj = _db.SanPhamChiTiets.Find(id);
            if (obj != null)
            {
                _db.SanPhamChiTiets.Remove(obj);
                _db.SaveChanges();
            }
        }
    }
} 