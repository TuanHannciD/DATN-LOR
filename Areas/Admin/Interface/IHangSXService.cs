using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IHangSXService
    {
        IEnumerable<HangSX> GetAll();
        HangSX GetById(Guid id);
        void Add(HangSX entity);
        void Update(HangSX entity);
        void Delete(Guid id);
    }
} 