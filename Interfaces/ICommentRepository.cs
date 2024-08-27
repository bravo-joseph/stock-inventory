using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Comment;
using resful_project.models;

namespace resful_project.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<Comment?> UpdateCommentAsync(int id, Comment comment);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}