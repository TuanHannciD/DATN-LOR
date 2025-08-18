using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IKichThuocService
    {
        Task<ApiResponse<IEnumerable<KichThuoc>>> GetAll();
        KichThuoc? GetById(Guid id);
        void Add(KichThuoc entity);
        void Update(KichThuoc entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
