using EagleRock.Api.BackgroundServices;
using EagleRock.Api.Data;
using EagleRock.Api.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services
builder.Services.AddDbContext<DbContextClass>(options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEagleBotRecordValidator, EagleBotRecordValidator>();
builder.Services.AddScoped<IEagleBotRepository, EagleBotRepository>();
//configure redis cache
builder.Services.AddSingleton<IRedisCacheService, RedisCacheService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
    options.InstanceName = "EagleBot";
});

//builder.Services.AddHostedService<SeedEagleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }