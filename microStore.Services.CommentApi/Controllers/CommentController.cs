using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microStore.Services.CommentApi.Data;
using microStore.Services.CommentApi.Models;
using microStore.Services.CommentApi.Models.DTO;
using System.Xml.Linq;

namespace microStore.Services.CommentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;

        public CommentController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{productId:int}/{page:int}/{size:int}")]
        public object GetCommentsByProductId(int productId, int page, int size)
        {
            //.Skip((page - 1) * size).Take(size)
            try
            {
                var comments = _db.CommentHeader.Where(x => x.ProductId == productId)
                    .Include(c => c.Comments).First();
                int[] scoreSums = new int[5];

                foreach (var comment in comments.Comments)
                {

                    int score = comment.Score;
                    if (score >= 1 && score <= 5)
                    {

                        scoreSums[score - 1] += 1;
                    }
                }
                comments.ScoreList = scoreSums;

                if (comments != null)
                {
                    comments.Comments = comments.Comments.Skip((page - 1) * size).Take(size);
                    _response.Data = _mapper.Map<CommentHeader>(comments);

                }

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }

        [HttpPost]
        public async Task<object> Post([FromBody] CommentHeaderDTO commentDTO)
        {
            try
            {
                CommentHeader commentHeader = _mapper.Map<CommentHeader>(commentDTO);
                var fssgd = commentHeader;
                if (commentDTO.CommentHeaderId == 0)
                {

                    _db.CommentHeader.Add(commentHeader);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    _db.CommentHeader.Update(commentHeader);
                    await _db.SaveChangesAsync();

                }
                commentDTO.Comments.First().CommentHeaderId = commentHeader.CommentHeaderId;
                //var cartdetails = _mapper.Map<CartDetails>(cartDTO.CartDetails.First());
                _db.Comments.Add(commentHeader.Comments.First());
                await _db.SaveChangesAsync();
                _response.Data = commentHeader;
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;
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
