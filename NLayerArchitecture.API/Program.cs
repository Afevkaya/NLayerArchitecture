using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<NLayerArchitectureDbContext>(options =>
{
    var connectionStrings = builder.Configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
    options.UseSqlServer(connectionStrings!.SqlServer);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();