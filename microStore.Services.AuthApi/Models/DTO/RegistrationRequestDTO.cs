namespace microStore.Services.AuthApi.Models.DTO
{
    public class RegistrationRequestDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; } = "0";
        public string Email { get; set; }
        public string Role { get; set; } = "CUSTOMER";
    }
}
