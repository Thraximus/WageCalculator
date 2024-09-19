using AspNetCoreRateLimit;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using WageCalculatorBackend.AppRepositories;
using WageCalculatorBackend.DbData;
using WageCalculatorBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// DB CONNECTING
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("JAWSDB_URL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35))));


// Rate limmiting
builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));



builder.Services.AddScoped<ITimeRuleRepository, CalculationRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();
app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
