using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conexion = builder.Configuration.GetConnectionString("conexion_bd");

builder.Services.AddDbContext<BD_HAPPY_PETContext>(opt => opt.UseSqlServer(conexion));

// Add services to the container.
builder.Services.AddScoped<AutenticacionDAO>();
builder.Services.AddScoped<CategoriaDAO>();
builder.Services.AddScoped<MarcaDAO>();
builder.Services.AddScoped<ProductoDAO>();
builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<VendedorDAO>();

// Configuramos el cors para permitir todas las rutas
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

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

// Configurar el pipeline HTTP
app.UseRouting();

// Configuramos el cors para permitir todas las rutas
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
