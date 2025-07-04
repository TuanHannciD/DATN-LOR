using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class SizeService : ISizeService
    {
        private readonly ApplicationDbContext _db;
        public SizeService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Size> GetAll() => _db.Sizes.ToList();
        public Size GetById(Guid id) => _db.Sizes.Find(id);
        public void Add(Size entity)
        {
            _db.Sizes.Add(entity);
            _db.SaveChanges();
        }
        public void Update(Size entity)
        {
            _db.Sizes.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var obj = _db.Sizes.Find(id);
            if (obj != null)
            {
                _db.Sizes.Remove(obj);
                _db.SaveChanges();
            }
        }
    }
} 