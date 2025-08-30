using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Applicaiton.Activities.Queries;
using Application.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Persistence.AppDbContext>(op =>
{
    op.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityList.Handler>());
builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
//builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetActivityDetails.Handler>());

var app = builder.Build();


// Configure the HTTP request pipeline.
//app.UseHttpsRedirection();
//app.UseAuthorization();
//CORS should be added before mapping controllers
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));

app.MapControllers();

//Uses dependency injection
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    AppDbContext context = services.GetRequiredService<AppDbContext>();
    //Will create database if it doesnt already exist
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch (System.Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error occurred during migration!!");
    throw;
}

app.Run();
