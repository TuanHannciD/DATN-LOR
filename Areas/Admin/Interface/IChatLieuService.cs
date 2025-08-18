using System;
using System.Collections.Generic;
using AuthDemo.Common;
using AuthDemo.Models;

namespace AuthDemo.Areas.Admin.Interface
{
    public interface IChatLieuService
    {
        Task<ApiResponse<IEnumerable<ChatLieu>>> GetAll();
        ChatLieu? GetById(Guid id);
        void Add(ChatLieu entity);
        void Update(ChatLieu entity);
        ApiResponse<string> Delete(Guid id);
    }
}
