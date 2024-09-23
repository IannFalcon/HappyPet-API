using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conexion = builder.Configuration.GetConnectionString("conexion_bd");

builder.Services.AddDbContext<BD_HAPPY_PETContext>(opt => opt.UseSqlServer(conexion));

// Add services to the container.
builder.Services.AddScoped<AutenticacionDAO>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
