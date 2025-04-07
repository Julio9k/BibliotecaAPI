using System.Text.Json.Serialization;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opciones=>
opciones.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AplicationDbContext>(opciones => 
                                    opciones.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

// Add middlewares to the container

app.Use(async (contexto, next) =>
{
    var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Petición: {contexto.Request.Method} {contexto.Request.Path}");
    await next.Invoke();
    logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
});
app.MapControllers();

app.Run();
