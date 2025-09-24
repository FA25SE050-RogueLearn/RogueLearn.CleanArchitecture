using MyMicroservice.Api.Extensions;
using MyMicroservice.Api.Middleware;
using MyMicroservice.Infrastructure.Extensions;
using MyMicroservice.Infrastructure.Logging;
using Serilog;

// Configure Serilog
Log.Logger = SerilogConfiguration.CreateLogger();

try
{
    Log.Information("Starting MyMicroservice API");

    var builder = WebApplication.CreateBuilder(args);

    // Add Serilog
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddApplication();
    builder.Services.AddInfrastructureServices(builder.Configuration);
    builder.Services.AddApiServices();

    var app = builder.Build();

    // Configure the HTTP request pipeline
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyMicroservice API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("MyMicroservice API started successfully");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "MyMicroservice API terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
