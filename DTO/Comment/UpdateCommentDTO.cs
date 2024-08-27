using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace resful_project.DTO.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Title Cannot be over 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3, ErrorMessage = "Content must be at least 3 characters")]
        [MaxLength(280, ErrorMessage = "Content Cannot be over 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}