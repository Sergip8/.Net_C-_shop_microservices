using EventBusMessages.Events.Contracts;
using HealthChecks.UI.Client;
using MassTransit;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using microStore.Services.OrderApi.Data;
using microStore.Services.OrderApi.Helpers;
using microStore.Services.OrderApi.Service;
using microStore.Services.OrderApi.Service.IService;
using Serilog;



Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Seq("http://seq:5341")
        .CreateLogger();
try
{
    Log.Information("Starting application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    ConfigurationManager configuration = builder.Configuration;
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddDbContext<AppDbContext>(optionsAction: options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
    });
    builder.Services.AddHealthChecks()
                 .AddCheck("self", () => HealthCheckResult.Healthy(), new string[] { "OrderAPI" })
                 .AddCheck("OrderingDB-check", new SqlConnectionHealthCheck(
                             builder.Configuration.GetConnectionString("SqlConnection")),
                             HealthStatus.Unhealthy, new string[] { "OrderDB" });
    builder.Services.AddMassTransit(x =>
    {
        x.AddRequestClient<ValidateUserRequest>();
        x.AddRequestClient<ValidateInventoryRequest>();

        x.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(configuration["EventBusSettings:HostAddress"]);
            cfg.ConfigureEndpoints(context);
        });
    });
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.WebHost.UseUrls("http://0.0.0.0:8080");
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        //using (var scope = app.Services.CreateScope())
        //{
        //    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        //    if (_db.Database.GetPendingMigrations().Count() > 0)
        //    {
        //        _db.Database.Migrate();
        //    }
        //}
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
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


