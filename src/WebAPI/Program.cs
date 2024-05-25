using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LoggingDbContext>(o =>
{
    o.UseSqlServer("Server=localhost,1433;User Id=sa;Database=master;Password=yourStrong(!)Password;TrustServerCertificate=True;");
},
    contextLifetime: ServiceLifetime.Transient,
    optionsLifetime: ServiceLifetime.Singleton);

builder.Services.AddLogging();

var app = builder.Build();

await app.Services.GetRequiredService<LoggingDbContext>().Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/echo", ([FromQuery] string phrase) =>
{
    return phrase;
})
.WithName("Echo")
.WithOpenApi();

app.Run();
