using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IThuongHieuService
    {
        Task<ApiResponse<IEnumerable<ThuongHieu>>> GetAll();
        Task<ApiResponse<IEnumerable<ThuongHieu>>> GetAllDelete();
        ThuongHieu? GetById(Guid id);
        Task<ApiResponse<CreateHangSanXuat>> AddAsync(CreateHangSanXuat create);
        Task<ApiResponse<string>> Update(ThuongHieu entity);
        Task<ApiResponse<string>> Delete(Guid id);
        Task<ApiResponse<string>> Restore(Guid id);
    }
}
