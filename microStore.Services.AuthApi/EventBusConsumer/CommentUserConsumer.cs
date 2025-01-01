using MassTransit;
using microStore.Services.AuthApi.Models.DTO;
using microStore.Services.AuthApi.Data;
using Microsoft.EntityFrameworkCore;
using EventBusMessages.Events.Contracts;

namespace microStore.Services.AuthApi.EventBusConsumer
{
    public class CommentUserConsumer : IConsumer<GetUserDetailsRequest>
    {
        private readonly ILogger<CommentUserConsumer> _logger;
        private readonly AppDbContext _db;

        public CommentUserConsumer(AppDbContext db, ILogger<CommentUserConsumer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GetUserDetailsRequest> context)
        {
            Console.WriteLine(context);
            var user = await _db.Users.Where(p => context.Message.UserId.Contains(p.Id)).ToListAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            await context.RespondAsync(
                new GetUserDetailsResponseList
                {
                    UserDetails = user.Select(user =>
                {
                    return new GetUserDetailsResponse
                    {
                        UserId = user.Id,
                        Name = user.Name,
                        Email = user.Email
                    };
                }
                ).ToList()
                }
                );
            _logger.LogInformation("CommentUserConsumer consumed successfully ");
        }
    }
}
