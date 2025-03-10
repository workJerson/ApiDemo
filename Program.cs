using ApiTest1.Context;
using ApiTest1.Contracts;
using ApiTest1.Repostories;
using ApiTest1.Services;
using Microsoft.EntityFrameworkCore;

// Scaffold Script:

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Service binding
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarBrandService, CarBrandService>();

// Repositories binding
builder.Services.AddScoped<ICarRepository, CarRepository>();

//Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Database Configuration
var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
string connectionString = "server=localhost; port=3306; database=api-test; user=root; password=root@2024; SslMode=Required;Allow User Variables=true;";

builder.Services.AddDbContext<DatabaseContext>(
            dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion).EnableDetailedErrors()
        );

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
