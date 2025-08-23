using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IDanhMucService
    {
        Task<ApiResponse<IEnumerable<DanhMuc>>> GetAll();
        Task<ApiResponse<IEnumerable<DanhMuc>>> GetAllDelete();
        DanhMuc? GetById(Guid id);
        Task<ApiResponse<CreateDanhMuc>> AddAsync(CreateDanhMuc createChatLieu);
        void Update(DanhMuc entity);
        Task<ApiResponse<string>> Delete(Guid id);
        Task<ApiResponse<string>> Restore(Guid id);
    }
}
