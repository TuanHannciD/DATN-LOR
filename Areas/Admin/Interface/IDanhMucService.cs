using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IDanhMucService
    {
        Task<ApiResponse<IEnumerable<DanhMuc>>> GetAll();
        DanhMuc? GetById(Guid id);
        Task<ApiResponse<CreateDanhMuc>> AddAsync(CreateDanhMuc createChatLieu);
        void Update(DanhMuc entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
