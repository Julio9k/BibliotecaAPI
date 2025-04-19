namespace BibliotecaAPI.DTOs
{
    public class AutorConLibrosDTO:AutorDTO
    {
        public List<LibroDTO> Libro { get; set; } = new List<LibroDTO>();
    }
}
