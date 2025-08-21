using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using AuthDemo.Models.ViewModels;
using static AuthDemo.Models.ViewModels.GiayVM;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IGiayService
    {
        IEnumerable<Giay> GetAll();
        Giay? GetById(Guid id);
        Task<ApiResponse<GiayCreate>> AddAsync(GiayCreate giayCreate);
        void Update(Giay sp);
        Task<ApiResponse<string>> Delete(Guid id);
        IEnumerable<GiayFullInfoVM> GetGiayFullInfoList();
    }
}
