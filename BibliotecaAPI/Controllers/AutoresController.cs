using BibliotecaAPI.Datos;
using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaAPI.Controllers
{

    [ApiController]
    [Route("")]
    public class AutoresController: ControllerBase
    {
        private readonly AplicationDbContext context;

        public AutoresController(AplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]

        public async Task<IEnumerable<Autor>> Get()
        {
            return await context.Autores.ToListAsync();
        }
       /* public IEnumerable<Autor> Get()

        {
            return new List<Autor>
            {
                new Autor{Id=1, Nombre="Felipe"},
                new Autor{Id=2, Nombre="Claudia"}
            };

        }*/

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }
        
    }
}
