using AutoMapper;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/libros/{libroId:int}/comentarios")]
    public class ComentariosController:ControllerBase
    {
        private readonly AplicationDbContext context;
        private readonly IMapper mapper;

        public ComentariosController(AplicationDbContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<ComentarioDTO>>> Get(int libroId)
        {
            var existeLibro = await context.Libros.AnyAsync(x => x.Id == libroId);
            if(!existeLibro)
            {
                return NotFound();
            }

            var comentarios = await context.Comentarios
                .Where(x => x.LibroId == libroId)
                .OrderByDescending(x => x.FechaPublicacion)
                .ToListAsync();

            return mapper.Map<List<ComentarioDTO>>(comentarios);
        }
    }
}
