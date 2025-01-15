using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microStore.Services.CommentApi.Data;
using microStore.Services.CommentApi.Models;
using microStore.Services.CommentApi.Models.DTO;
using microStore.Services.CommentApi.Service;
using microStore.Services.CommentApi.Service.IService;
using System.Xml.Linq;

namespace microStore.Services.CommentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CommentController> _logger;

        private readonly ICommentService _commentService;

        public CommentController(AppDbContext db, ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [Route("{productId:int}/{page:int}/{size:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentsByProductId(int productId, int page, int size)
        {
            var res = await _commentService.GetCommentsByProductId(productId, page, size);

            if (res.Data == null)
            {
                _logger.LogWarning("Error al ver el comentario");
                return Ok(new CommentHeader());
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StoreComment([FromBody] CommentHeaderWriteDTO commentDTO)
        {
            var res = await _commentService.StoreComment(commentDTO);

            if (res.Data == null)
            {
                _logger.LogWarning("Error al crear el comentario");
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
        }
        [HttpGet]
        [Route("page/{commentHeaderId:int}/{page:int}/{size:int}")]
        public object GetCommentsByCommentHeaderId(int commentHeaderId, int page, int size)
        {
            //.Skip((page - 1) * size).Take(size)
            try
            {
                var comments = _db.Comments.Where(x => x.CommentHeaderId == commentHeaderId).Skip((page - 1) * size).Take(size);
                return comments;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
