using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using microStore.ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var settingsSection = builder.Configuration.GetSection("ApiSettings");

var secret = settingsSection.GetValue<string>("Secret");
var issuer = settingsSection.GetValue<string>("Issuer");
var audience = settingsSection.GetValue<string>("Audence");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience
        };
    });
//builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").WithMethods("PUT", "DELETE", "GET", "POST").AllowAnyHeader();
                      });
});
// Add services to the container.
if (builder.Environment.EnvironmentName.ToString().ToLower().Equals("production"))
{
    builder.Configuration.AddJsonFile("ocelot.Production.json", optional: false, reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
}
builder.WebHost.UseUrls("http://*:8080");

builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseOcelot().GetAwaiter().GetResult();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
