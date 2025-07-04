using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class HangSXService : IHangSXService
    {
        private readonly ApplicationDbContext _db;
        public HangSXService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<HangSX> GetAll() => _db.HangSXs.ToList();
        public HangSX GetById(Guid id) => _db.HangSXs.Find(id);
        public void Add(HangSX entity)
        {
            _db.HangSXs.Add(entity);
            _db.SaveChanges();
        }
        public void Update(HangSX entity)
        {
            _db.HangSXs.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var obj = _db.HangSXs.Find(id);
            if (obj != null)
            {
                _db.HangSXs.Remove(obj);
                _db.SaveChanges();
            }
        }
    }
} 