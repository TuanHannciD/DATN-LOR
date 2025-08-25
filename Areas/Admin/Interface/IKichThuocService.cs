using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IKichThuocService
    {
        Task<ApiResponse<IEnumerable<KichThuoc>>> GetAll();
        Task<ApiResponse<IEnumerable<KichThuoc>>> GetAllDelete();
        KichThuoc? GetById(Guid id);
        Task<ApiResponse<CreateKichThuoc>> AddAsync(CreateKichThuoc create);
        Task<ApiResponse<string>> Update(KichThuoc entity);
        Task<ApiResponse<string>> Delete(Guid id);
        Task<ApiResponse<string>> Restore(Guid id);
    }
}
