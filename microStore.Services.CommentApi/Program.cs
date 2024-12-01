using Microsoft.EntityFrameworkCore;
using microStore.Services.CommentApi;
using microStore.Services.CommentApi.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

builder.Services.AddAutoMapper(typeof(CouponMappingProfile));
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

app.UseAuthorization();

app.MapControllers();
//ApplyMigration();
app.Run();

//void ApplyMigration()
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//        if (_db.Database.GetPendingMigrations().Count() > 0)
//        {
//            _db.Database.Migrate();
//        }
//    }
//}
