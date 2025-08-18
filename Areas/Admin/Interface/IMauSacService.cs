using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IMauSacService
    {
        Task<ApiResponse<IEnumerable<MauSac>>> GetAll();
        MauSac? GetById(Guid id);
        void Add(MauSac entity);
        void Update(MauSac entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
