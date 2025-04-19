using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class ComentarioDTO
    {
        public Guid Id { get; set; }
       
        public required String Cuerpo { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
