using Microsoft.AspNetCore.Identity;

namespace microStore.Services.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
