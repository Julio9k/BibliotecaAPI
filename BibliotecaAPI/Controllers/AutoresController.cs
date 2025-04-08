using System.Collections.Concurrent;
using BibliotecaAPI.Datos;
using BibliotecaAPI.DTOs;
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
        public async Task<IEnumerable<AutorDTO>> Get()
        {
       
            
            var autores= await context.Autores.ToListAsync();
            var autoresDTO = autores.Select(autor =>
                                                  new AutorDTO 
                                                  { Id = autor.Id, 
                                                    NombreCompleto = $"{autor.Nombres} {autor.Apellidos}"
                                                  });
            return autoresDTO;
        }



        [HttpGet("{id:int}",Name ="ObtenerAutor")]
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
            return CreatedAtRoute("ObtenerAutor",new {id=autor.Id},autor);
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
