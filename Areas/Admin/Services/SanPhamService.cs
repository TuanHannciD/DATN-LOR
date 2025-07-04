using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly ApplicationDbContext _db;
        public SanPhamService(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SanPham> GetAll() => _db.SanPhams.ToList();

        public SanPham GetById(Guid id) => _db.SanPhams.Find(id);

        public void Add(SanPham sp)
        {
            _db.SanPhams.Add(sp);
            _db.SaveChanges();
        }

        public void Update(SanPham sp)
        {
            _db.SanPhams.Update(sp);
            _db.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var sp = _db.SanPhams.Find(id);
            if (sp != null)
            {
                _db.SanPhams.Remove(sp);
                _db.SaveChanges();
            }
        }
    }
} 