using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AplicationDbContext>(opciones => 
                                    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Add middlewares to the container

app.MapControllers();

app.Run();
