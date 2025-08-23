using System;
using System.Collections.Generic;
using AuthDemo.Common;

using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.ChiTietGiayVM;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IChiTietGiayService
    {
        List<ChiTietGiay> GetAll();
        Task<ApiResponse<IEnumerable<IndexVM>>> GetAllDelete();
        Task<ApiResponse<IEnumerable<IndexVM>>> GetAllIndexVMAsync();
        ChiTietGiay? GetById(Guid id);
        Task<ApiResponse<CreateVM>> Add(CreateVM editVM);

        ApiResponse<string> Update(EditVM entity);
        ApiResponse<string> Delete(Guid id);
        Task<ApiResponse<string>> Restore(Guid id);

    }
}
