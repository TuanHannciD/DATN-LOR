using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IMauSacService
    {
        Task<ApiResponse<IEnumerable<MauSac>>> GetAll();
        MauSac? GetById(Guid id);
        Task<ApiResponse<CreateMauSac>> AddAsync(CreateMauSac create);
        void Update(MauSac entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
