using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IChiTietGiayService
    {
        IEnumerable<ChiTietGiay> GetAll();
        ChiTietGiay? GetById(Guid id);
        void Add(ChiTietGiay entity);
        void Update(ChiTietGiay entity);
        void Delete(Guid id);
    }
} 