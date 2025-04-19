using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.Entidades
{
    public class Comentario
    {
        public Guid Id { get; set; }
        [Required]
        public required String Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int LibroId { get; set; }
        public Libro? Libro { get; set; }

    }
}
