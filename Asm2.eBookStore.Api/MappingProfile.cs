using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.EntityModel;
using AutoMapper;

namespace Asm2.eBookStore.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Publisher, PublisherDto>().ReverseMap();
        CreateMap<Publisher, PublisherUpdateDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
    }
}
