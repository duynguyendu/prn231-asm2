using Asm2.eBookStore.Api.Dto.Response;

namespace Asm2.eBookStore.Client.Models.Authors;

public class AuthorsViewModel : IPagingResult
{
    public IList<AuthorDto> Authors { get; set; } = new List<AuthorDto>();
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}
