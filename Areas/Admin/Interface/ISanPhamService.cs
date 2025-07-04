using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface ISanPhamService
    {
        IEnumerable<SanPham> GetAll();
        SanPham GetById(Guid id);
        void Add(SanPham sp);
        void Update(SanPham sp);
        void Delete(Guid id);
    }
}
