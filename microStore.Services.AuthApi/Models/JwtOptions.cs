﻿namespace microStore.Services.AuthApi.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audence { get; set; }
    }
}
