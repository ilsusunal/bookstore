using AutoMapper;
using Bookstore.Domain.Entities;
using Bookstore.Api.Models.Books;

namespace Bookstore.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity -> DTO
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

        // DTO -> Entity
        CreateMap<CreateBookRequest, Book>();
        CreateMap<UpdateBookRequest, Book>();
    }
}