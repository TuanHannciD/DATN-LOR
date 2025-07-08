using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IGiayService
    {
        IEnumerable<Giay> GetAll();
        Giay? GetById(Guid id);
        void Add(Giay sp);
        void Update(Giay sp);
        void Delete(Guid id);
    }
}
