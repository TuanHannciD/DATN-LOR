using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class MauSacService : IMauSacService
    {
        private readonly ApplicationDbContext _db;
        public MauSacService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<MauSac> GetAll() => _db.MauSacs.ToList();
        public MauSac GetById(Guid id) => _db.MauSacs.Find(id);
        public void Add(MauSac entity)
        {
            _db.MauSacs.Add(entity);
            _db.SaveChanges();
        }
        public void Update(MauSac entity)
        {
            _db.MauSacs.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var obj = _db.MauSacs.Find(id);
            if (obj != null)
            {
                _db.MauSacs.Remove(obj);
                _db.SaveChanges();
            }
        }
    }
} 