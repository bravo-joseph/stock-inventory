using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using resful_project.data;
using resful_project.DTO.Comment;
using resful_project.Interfaces;
using resful_project.models;

namespace resful_project.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var commentsmodel = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
            if (commentsmodel == null)
            {
                return null;
            }
            _context.Comments.Remove(commentsmodel);
            await _context.SaveChangesAsync();
            return commentsmodel;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.Include(user => user.AppUser).ToListAsync();
        }
        public async Task<Comment?> GetByIdAsync(int id)
        {
            var stock = await _context.Comments.Include(user => user.AppUser).FirstOrDefaultAsync(comment => comment.Id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment comment)
        {
            var commentfromDB = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
            if (commentfromDB == null)
            {
                return null;
            }
            commentfromDB.Title = comment.Title;
            commentfromDB.Content = comment.Content;

            await _context.SaveChangesAsync();
            return commentfromDB;
        }
    }
}