using AutoMapper;
using Google;
using HealthChecks.UI.Client;
using InventoryServiceClient;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using microStore.Services.ProductApi;
using microStore.Services.ProductApi.Contracts;
using microStore.Services.ProductApi.Data;
using microStore.Services.ProductApi.Extensions;
using microStore.Services.ProductApi.Helpers;
using microStore.Services.ProductApi.Service;
using microStore.Services.ProductApi.Service.IService;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Seq("http://seq:5341")
        .CreateLogger();

try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    // Add services to the container.
    builder.Services.AddDbContext<AppDbContext>(optionsAction: options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 2,
        maxRetryDelay: TimeSpan.FromSeconds(15),
        errorNumbersToAdd: null);

    });
    });
    builder.Services.AddHealthChecks()
                   .AddCheck("self", () => HealthCheckResult.Healthy(), new string[] { "ProductAPI" })
                   .AddCheck("ProductDB-check", new SqlConnectionHealthCheck(
                               builder.Configuration.GetConnectionString("SqlConnection")),
                               HealthStatus.Unhealthy, new string[] { "ProductDB" });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("http://localhost:4200").WithMethods("PUT", "DELETE", "GET", "POST").AllowAnyHeader();
                          });
    });

    builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpClient("Comment", p => p.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CommentApi"]));
    builder.Services.AddScoped<IInventoryService, InventoryServices>();
    builder.Services.AddScoped<ICommentService, CommentService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<UploadImages>();
    builder.Services.AddAuthorization();
    builder.WebHost.UseUrls("http://0.0.0.0:8080");
    builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
    builder.Services.AddGrpcClient<InventoryProto.InventoryProtoClient>(o =>
    {
        o.Address = new Uri("http://inventory:8080");
    });
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        //var application = app.Services.CreateScope().ServiceProvider.GetRequiredService<DbContext>();

        //var pendingMigrations = await application.Database.GetPendingMigrationsAsync();
        //if (pendingMigrations != null)
        //    await application.Database.MigrateAsync();
    }

    app.UseCors(MyAllowSpecificOrigins);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    //using (var scope = app.Services.CreateScope())
    //{
    //    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //    if (_db.Database.GetPendingMigrations().Any())
    //    {
    //        _db.Database.Migrate();
    //    }
    //}
    app.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });

    app.UseHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

