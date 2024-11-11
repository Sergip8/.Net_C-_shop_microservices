using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using microStore.Services.ProductApi;
using microStore.Services.ProductApi.Data;
using microStore.Services.ProductApi.Extensions;
using microStore.Services.ProductApi.Service;
using microStore.Services.ProductApi.Service.IService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(optionsAction: options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").WithMethods("PUT", "DELETE", "GET", "POST").AllowAnyHeader();
                      });
});
//IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
//builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("Inventory", p => p.BaseAddress = new Uri(builder.Configuration["ServiceUrls:InventoryApi"]));
builder.Services.AddHttpClient("Comment", p => p.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CommentApi"]));

builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
//builder.AddAppAuthentication();
builder.Services.AddAuthorization();
builder.WebHost.UseUrls("https://localhost:8003;http://localhost:5011");
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
ApplyMigration();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}
