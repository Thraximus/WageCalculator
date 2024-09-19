using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using WageCalculatorBackend.AppRepositories;
using WageCalculatorBackend.DbData;
using WageCalculatorBackend.Repositories;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Services.AddControllers();
var connectionString = Environment.GetEnvironmentVariable("JAWSDB_URL");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 35))));

builder.Services.AddScoped<ICalculationRepository, CalculationRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
