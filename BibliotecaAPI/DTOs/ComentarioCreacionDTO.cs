using System.ComponentModel.DataAnnotations;

namespace BibliotecaAPI.DTOs
{
    public class ComentarioCreacionDTO
    {
        [Required]
        public required String Cuerpo { get; set; }
    }
}
