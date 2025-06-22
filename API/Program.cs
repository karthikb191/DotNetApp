using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Persistence.AppDbContext>(op =>
{
    op.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

//Uses dependency injection
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    AppDbContext context = services.GetRequiredService<AppDbContext>();
    //Will create database if it doesnt already exist
    await context.Database.MigrateAsync();
    await DbInitializer.SeedDate(context);
}
catch (System.Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error occurred during migration!!");
    throw;
}

app.Run();
