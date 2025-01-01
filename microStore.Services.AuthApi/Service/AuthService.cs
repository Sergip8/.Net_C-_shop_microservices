using Microsoft.AspNetCore.Identity;
using microStore.Services.AuthApi.Data;
using microStore.Services.AuthApi.Models;
using microStore.Services.AuthApi.Models.DTO;
using microStore.Services.AuthApi.Service.IService;

namespace microStore.Services.AuthApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private ResponseDTO _responseDTO;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _responseDTO = new ResponseDTO();
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == loginRequestDTO.Email.ToLower());
            bool isValidUser = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (!isValidUser)
            {
                _responseDTO.Success = false;
                _responseDTO.Data = null;
                _responseDTO.Message = "Nombre de usuario o password incorrectos";
            }
            else
            {
                var role = await _userManager.GetRolesAsync(user);
                string token = _jwtGenerator.GenerateJwt(user, role);
                UserDTO userDTO = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    Role = role
                };
                LoginResponseDTO loginResponse = new LoginResponseDTO
                {
                    User = userDTO,
                    Token = token,
                };
                _responseDTO.Success = true;
                _responseDTO.Data = loginResponse;
                _responseDTO.Message = "Bienvenido";

            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber,

            };
            try
            {
                var res = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (res.Succeeded)
                {
                    var userRes = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.Email);
                    if (!_roleManager.RoleExistsAsync(registrationRequestDTO.Role).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(registrationRequestDTO.Role)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(userRes, registrationRequestDTO.Role);
                    UserDTO userDTO = new()
                    {
                        Email = userRes.Email,
                        ID = userRes.Id,
                        Name = userRes.Name,
                        Role = [registrationRequestDTO.Role],
                    };
                    _responseDTO.Success = true;
                    _responseDTO.Data = userDTO;
                    _responseDTO.Message = "Usuario Registrado correctemente";

                }
                else
                {
                    _responseDTO.Success = false;
                    _responseDTO.Data = null;
                    _responseDTO.Message = res.Errors.FirstOrDefault().Description;

                }
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Data = null;
                _responseDTO.Message = "No se pudo completar el registro";

            }
            return _responseDTO;
        }

        public async Task<ResponseDTO> AssingRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                _responseDTO.Success = true;
                _responseDTO.Data = user;
                _responseDTO.Message = "Rol Asignado al usuario";

            }
            else
            {
                _responseDTO.Success = false;
                _responseDTO.Data = null;
                _responseDTO.Message = "Un error ha ocurrido";
            }
            return _responseDTO;
        }
        public ResponseDTO getUsersByIds(List<string> ids)
        {
            var user = _db.Users.Where(p => ids.Contains(p.Id)).ToList();
            if (user != null)
            {
                _responseDTO.Success = true;
                _responseDTO.Data = user;
                _responseDTO.Message = "Rol Asignado al usuario";
            }
            else
            {
                _responseDTO.Success = false;
                _responseDTO.Data = null;
                _responseDTO.Message = "Un error ha ocurrido";
            }
            return _responseDTO;
        }
    }
}
