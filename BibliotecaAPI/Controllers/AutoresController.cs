using System.Collections.Concurrent;
using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{

    [ApiController]
    [Route("")]
    public class AutoresController : ControllerBase
    {
        private readonly AplicationDbContext context;

        public AutoresController(AplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]

        public async Task<IEnumerable<Autor>> Get()
        {
            
            return await context.Autores
                                        .Include(a => a.Libros)
                                         .ToListAsync();
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores
                .Include(x=>x.Libros)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (autor is null)
            {
                return NotFound();

            }

            return autor;

        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put( int id, Autor autor)
            {
            if (id!= autor.Id)
            {
                return BadRequest("Los id deben de considir");
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
            }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var registroBorrados = await context.Autores.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registroBorrados==0)
            {
                return NotFound();            
            }

            return Ok();

        }



    }
}
