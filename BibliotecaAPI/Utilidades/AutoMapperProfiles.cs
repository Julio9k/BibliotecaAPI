using AutoMapper;
using BibliotecaAPI.DTOs;
using BibliotecaAPI.Entidades;

namespace BibliotecaAPI.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(dto => dto.NombreCompleto,
                config => config.MapFrom(autor => $"{autor.Nombres} {autor.Apellidos}"));
            CreateMap<AutorCreacionDTO, Autor>();


            CreateMap<Autor, AutorConLibrosDTO>()
                .ForMember(dto => dto.NombreCompleto,
                config => config.MapFrom(autor => $"{autor.Nombres} {autor.Apellidos}"));
            CreateMap<AutorCreacionDTO, Autor>();

            CreateMap<Autor, AutorPatchDTO>().ReverseMap();

            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroCreacionDTO, Libro>();

            CreateMap<Libro, LibroConAutorDTO>()
                .ForMember(dto => dto.AutorNombre, config =>
                    config.MapFrom(ent => $"{ent.Autor!.Nombres} {ent.Autor.Apellidos}"));

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
            CreateMap<ComentarioPatchDTO, Comentario>().ReverseMap();
        }

        private string MapearNombreYApellido(Autor autor) => $"{autor.Nombres} {autor.Apellidos}";
    }
}
