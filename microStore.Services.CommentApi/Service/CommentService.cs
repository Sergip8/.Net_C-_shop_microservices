using microStore.Services.CommentApi.Models.DTO;
using microStore.Services.CommentApi.Data;
using microStore.Services.CommentApi.Service.IService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using microStore.Services.CommentApi.Models;
using System.Drawing;
using MassTransit;
using EventBusMessages.Events.Contracts;

namespace microStore.Services.CommentApi.Service
{
    public class CommentService : ICommentService
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetUserDetailsRequest> _userDetailsClient;

        public CommentService(AppDbContext db, IMapper mapper, IRequestClient<GetUserDetailsRequest> userDetailsClient)
        {
            _userDetailsClient = userDetailsClient;
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }
        public async Task<object> GetCommentsByCommentHeaderId(int commentHeaderId, int page, int size)
        {
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

        public async Task<ResponseDTO> GetCommentsByProductId(int productId, int page, int size)
        {
            try
            {
                var comments = await _db.CommentHeader.Where(x => x.ProductId == productId)
                    .Include(c => c.Comments).FirstAsync();
                var ids = comments.Comments.Select(c => c.CommentUserId).Distinct().ToList();
                //Console.WriteLine(ids.Last());
                var response = await _userDetailsClient.GetResponse<GetUserDetailsResponseList>(
                        new GetUserDetailsRequest { UserId = ids }
                    );
                var userDictionary = response.Message.UserDetails.ToDictionary(u => u.UserId);
                Console.WriteLine(response);
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

                    var commentsDto = comments.Comments.Select(c =>
                    {
                        return new CommentDTO
                        {
                            CommentId = c.CommentId,
                            Title = c.Title,
                            Content = c.Content,
                            CreatedDate = c.CreatedDate,
                            Score = c.Score,
                            Votes = c.Votes,
                            CommentHeaderId = c.CommentHeaderId,
                            UserDetails = userDictionary.TryGetValue(c.CommentUserId, out var userDetails)
                             ? userDetails
                             : null
                        };
                    });
                    var headerDto = new CommentHeaderDTO
                    {
                        CommentHeaderId = comments.CommentHeaderId,
                        OverallScore = comments.OverallScore,
                        QtyForStar = comments.QtyForStar,
                        CommentCount = comments.CommentCount,
                        ProductId = comments.ProductId,
                        ScoreList = comments.ScoreList,
                        Comments = commentsDto,
                    };


                    _response.Data = headerDto;

                }

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> StoreComment(CommentHeaderDTO commentDTO)
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

        //public async Task<CommentDetails> GetCommentDetails(int commentId)
        //{
        //    // Obtener el comentario
        //    // var comment = await _dbContext.Comments.FindAsync(commentId);

        //    // Obtener detalles del usuario
        //    var response = await _userDetailsClient.GetResponse<UserDetails>(
        //        new GetUserDetailsRequest { UserId = comment.UserId }
        //    );

        //    return new CommentDetails
        //    {
        //        CommentId = comment.Id,
        //        ProductId = comment.ProductId,
        //        Content = comment.Content,
        //        CreatedAt = comment.CreatedAt,
        //        User = new UserDetails
        //        {
        //            UserId = response.Message.UserId,
        //            Name = response.Message.Name,
        //            Email = response.Message.Email
        //        }
        //    };
        //}
    }

}