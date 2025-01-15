using microStore.Services.AuthApi.Models;

namespace microStore.Services.AuthApi.Service.IService
{
    public interface IJwtGenerator
    {
        string GenerateJwt(ApplicationUser applicationUser, IList<string> roles);
    }
}
