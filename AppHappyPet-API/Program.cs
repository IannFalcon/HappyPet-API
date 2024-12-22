using Business;
using Data;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conexion = builder.Configuration.GetConnectionString("conexion_bd");

// Add services to the container.

builder.Services.AddDbContext<BdHappyPetContext>(opt => opt.UseSqlServer(conexion));

// DAO
builder.Services.AddScoped<AutenticacionDAO>();
builder.Services.AddScoped<CategoriaDAO>();
builder.Services.AddScoped<CarritoDAO>();
builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<ContadoresDAO>();
builder.Services.AddScoped<ClienteDireccionDAO>();
builder.Services.AddScoped<DetalleVentaDAO>();
builder.Services.AddScoped<MarcaDAO>();
builder.Services.AddScoped<ProductoDAO>();
builder.Services.AddScoped<ProveedorDAO>();
builder.Services.AddScoped<EmpleadoDAO>();
builder.Services.AddScoped<VentaDAO>();

// Servicios
builder.Services.AddScoped<AutenticacionService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<CarritoService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ClienteDireccionService>();
builder.Services.AddScoped<ContadoresService>();
builder.Services.AddScoped<DetalleVentaService>();
builder.Services.AddScoped<MarcaService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<ProveedorService>();
builder.Services.AddScoped<EmpleadoService>();
builder.Services.AddScoped<VentaService>();

// Controllers
builder.Services.AddControllers();

// Configuramos el cors para permitir todas las rutas
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

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
