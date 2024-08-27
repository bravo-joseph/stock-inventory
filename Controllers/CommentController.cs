using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using resful_project.DTO.Comment;
using resful_project.Extensions;
using resful_project.Interfaces;
using resful_project.Mapper;
using resful_project.models;

namespace resful_project.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockrepository;
        private readonly UserManager<AppUser> _userManager;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockrepository, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockrepository = stockrepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            var commentsDTO = comments.Select(x => x.ToCommentDTO());
            return Ok(commentsDTO);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDTO());
        }
        [HttpPost]
        [Route("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDTO createcommentdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockExists = await _stockrepository.IsStockExistAsync(stockId);
            if (stockExists == false)
            {
                return BadRequest("Stock does not Exist");
            }
            string username = User.GetUsername();
            AppUser appuser = await _userManager.FindByNameAsync(username);

            var commentmodel = createcommentdto.ToCommentFromCreateCommentDTO(stockId);
            commentmodel.AppUserId = appuser.Id;
            await _commentRepository.CreateCommentAsync(commentmodel);
            return CreatedAtAction(nameof(GetCommentById), new { Id = commentmodel.Id }, commentmodel.ToCommentDTO());
        }
        [HttpPut]
        [Route("{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDTO updatecommentdto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updatedcomment = await _commentRepository.UpdateCommentAsync(commentId, updatecommentdto.ToCommentFromUpdateCommentDTO());
            if (updatedcomment == null)
            {
                return NotFound();
            }
            return Ok(updatedcomment.ToCommentDTO());
        }
        [HttpDelete]
        [Route("{commentId:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            var comment = await _commentRepository.DeleteCommentAsync(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}