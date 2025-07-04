using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface ISanPhamChiTietService
    {
        IEnumerable<SanPhamChiTiet> GetAll();
        SanPhamChiTiet GetById(Guid id);
        void Add(SanPhamChiTiet entity);
        void Update(SanPhamChiTiet entity);
        void Delete(Guid id);
    }
} 