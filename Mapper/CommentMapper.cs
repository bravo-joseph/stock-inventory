using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using resful_project.DTO.Comment;
using resful_project.models;

namespace resful_project.Mapper
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDTO(this Comment commentmodel)
        {
            return new CommentDTO
            {
                Id = commentmodel.Id,
                Title = commentmodel.Title,
                Content = commentmodel.Content,
                CreatedOn = commentmodel.CreatedOn,
                StockId = commentmodel.StockId,
            };
        }
        public static Comment ToCommentFromCreateCommentDTO(this CreateCommentDTO createcommentdto, int stockId)
        {
            return new Comment
            {
                Title = createcommentdto.Title,
                Content = createcommentdto.Content,
                CreatedOn = DateTime.Now,
                StockId = stockId,
            };
        }
        public static Comment ToCommentFromUpdateCommentDTO(this UpdateCommentDTO updatecommentdto)
        {
            return new Comment
            {
                Title = updatecommentdto.Title,
                Content = updatecommentdto.Content,
            };
        }

    }
}