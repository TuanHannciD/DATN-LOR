using System;
using System.Collections.Generic;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface ISizeService
    {
        IEnumerable<Size> GetAll();
        Size GetById(Guid id);
        void Add(Size entity);
        void Update(Size entity);
        void Delete(Guid id);
    }
} 