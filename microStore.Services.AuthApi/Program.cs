using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using microStore.Services.AuthApi.Data;
using microStore.Services.AuthApi.Models;
using microStore.Services.AuthApi.Service;
using microStore.Services.AuthApi.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(optionsAction: options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {


                          policy.WithOrigins("http://localhost:4200").WithMethods("PUT", "DELETE", "GET", "POST").AllowAnyHeader();
                      });
});
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://0.0.0.0:8080");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//ApplyMigration();
app.Run();
//void ApplyMigration()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//        if (_db.Database.GetPendingMigrations().Any())
//        {
//            _db.Database.Migrate();
//        }
//    }
//}

