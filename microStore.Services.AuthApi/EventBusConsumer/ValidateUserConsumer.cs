using EventBusMessages.Events.Contracts;
using MassTransit;
using microStore.Services.AuthApi.Data;
using microStore.Services.AuthApi.Models;
namespace microStore.Services.CommentApi.EventBusConsumer
{
    public class ValidateUserConsumer : IConsumer<ValidateUserRequest>
    {
        private readonly AppDbContext _db;
        public ValidateUserConsumer(AppDbContext db)
        {
            _db = db;
        }
        public async Task Consume(ConsumeContext<ValidateUserRequest> context)
        {
            var userId = context.Message.UserId;
            Console.WriteLine(userId);
            // Lógica para verificar si el usuario existe
            var isValid = CheckUserExists(userId); // Implementa esta lógica

            await context.RespondAsync(new ValidateUserResponse { IsValid = isValid });
        }

        private bool CheckUserExists(string userId)
        {
            return _db.Users.FirstOrDefault(u => u.Id == userId) is ApplicationUser user;
        }
    }
}
