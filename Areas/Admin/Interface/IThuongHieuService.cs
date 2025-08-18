using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IThuongHieuService
    {
        Task<ApiResponse<IEnumerable<ThuongHieu>>> GetAll();
        ThuongHieu? GetById(Guid id);
        void Add(ThuongHieu entity);
        void Update(ThuongHieu entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
