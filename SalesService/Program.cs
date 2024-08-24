using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;
using SalesService.Data;
using SalesService.Data.DbContexts;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionName = "DefaultConnection";
var connection = builder.Configuration.GetConnectionString(connectionName);
builder.Services.AddDbContext<SalesDbContext>(opt => opt.UseNpgsql(connection));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
