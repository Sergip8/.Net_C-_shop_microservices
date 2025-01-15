using MassTransit;
using microStore.Services.CommentApi.Models.DTO;
using microStore.Services.CommentApi.Data;
using Microsoft.EntityFrameworkCore;
using EventBusMessages.Events.Contracts;
using microStore.Services.CommentApi.Migrations;

namespace microStore.Services.AuthApi.EventBusConsumer
{
    public class CommentUserConsumer : IConsumer<GetUserDetailsResponseList>
    {
        private readonly ILogger<CommentUserConsumer> _logger;
        private readonly AppDbContext _db;
        private readonly IRequestClient<GetUserDetailsResponseList> _requestClient;

        public CommentUserConsumer(AppDbContext db, ILogger<CommentUserConsumer> logger, IRequestClient<GetUserDetailsResponseList> requestClient)
        {
            _requestClient = requestClient;
            _db = db;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetUserDetailsResponseList> context)
        {
            var comments = await _db.CommentHeader.Where(x => x.ProductId == 3)
                    .Include(c => c.Comments).FirstAsync();
            var ids = comments.Comments.Select(c => c.CommentUserId).Distinct().ToList();
            var response = await _requestClient.GetResponse<GetUserDetailsResponseList>(
                    new GetUserDetailsRequest { UserId = ids });

            //await context.Publish(new GetUserDetailsRequest { UserId = ids });
            _logger.LogInformation("CommentUserConsumer consumed successfully ");
        }
    }
}
