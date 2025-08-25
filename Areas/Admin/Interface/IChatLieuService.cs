using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;
using static AuthDemo.Models.ViewModels.VMCHUNG;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IChatLieuService
    {
        Task<ApiResponse<IEnumerable<ChatLieu>>> GetAll();
        Task<ApiResponse<IEnumerable<ChatLieu>>> GetAllDelete();
        ChatLieu? GetById(Guid id);
        Task<ApiResponse<CreateChatLieu>> AddAsync(CreateChatLieu createChatLieu);
        Task<ApiResponse<string>> Update(ChatLieu entity);
        Task<ApiResponse<string>> Delete(Guid id);
        Task<ApiResponse<string>> Restore(Guid id);
    }
}
