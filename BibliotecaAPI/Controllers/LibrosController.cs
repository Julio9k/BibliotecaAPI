﻿using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers


{
    [ApiController]
    [Route("api/libros")]

    public class LibrosController: ControllerBase
    {
        
        private readonly AplicationDbContext context;

        public LibrosController(AplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Libro>> Get()
        {
            return await context.Libros
                .Include(a => a.Autor)
                .ToListAsync();
        }
        [HttpGet("{id:int}",Name ="ObtenerLibro")]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            var libro = await context.Libros
                .Include(x=> x.Autor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (libro is null)
            {
                return NotFound();
            }

            return libro;

        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro) 
        {
            var existeAutor = await context.Autores.AnyAsync(x => x.Id == libro.AutorId);

            if (!existeAutor)
            {
               ModelState.AddModelError(nameof(libro.AutorId), $"El autor de id {libro.AutorId} no existe");

                return ValidationProblem();

            }

            context.Add(libro);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerLibro", new {id=libro.Id},libro);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put (int id, Libro libro)
        {
            if (id != libro.Id)
            {
                return BadRequest("Los ids deben de concidir");

            }
            context.Update(libro);
            await context.SaveChangesAsync();
            return Ok();


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete (int id)
        {
            var registrosBorrados = await context.Libros.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados==0)
            {
                return NotFound();

            }

            return Ok();

        }

    }
}
