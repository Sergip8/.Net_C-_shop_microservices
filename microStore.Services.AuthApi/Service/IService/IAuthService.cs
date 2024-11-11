using microStore.Services.AuthApi.Models.DTO;

namespace microStore.Services.AuthApi.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<ResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<ResponseDTO> AssingRole(string Email, string roleName);
    }
}
