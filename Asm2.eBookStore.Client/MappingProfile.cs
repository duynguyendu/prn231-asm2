using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using AutoMapper;

namespace Asm2.eBookStore.Client;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BookDto, BookUpdateDto>();
    }
}
