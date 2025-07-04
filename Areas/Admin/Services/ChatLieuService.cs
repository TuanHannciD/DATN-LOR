using System;
using System.Collections.Generic;
using System.Linq;
using AuthDemo.Models;
using AuthDemo.Data;
using AuthDemo.Areas.Admin.Interface;

namespace AuthDemo.Areas.Admin.Services
{
    public class ChatLieuService : IChatLieuService
    {
        private readonly ApplicationDbContext _db;
        public ChatLieuService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ChatLieu> GetAll() => _db.ChatLieus.ToList();
        public ChatLieu GetById(Guid id) => _db.ChatLieus.Find(id);
        public void Add(ChatLieu entity)
        {
            _db.ChatLieus.Add(entity);
            _db.SaveChanges();
        }
        public void Update(ChatLieu entity)
        {
            _db.ChatLieus.Update(entity);
            _db.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var obj = _db.ChatLieus.Find(id);
            if (obj != null)
            {
                _db.ChatLieus.Remove(obj);
                _db.SaveChanges();
            }
        }
    }
} 