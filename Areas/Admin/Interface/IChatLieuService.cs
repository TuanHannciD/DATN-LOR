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
        ChatLieu? GetById(Guid id);
        Task<ApiResponse<CreateChatLieu>> AddAsync(CreateChatLieu createChatLieu);
        void Update(ChatLieu entity);
        Task<ApiResponse<string>> Delete(Guid id);
    }
}
