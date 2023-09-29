using Asm2.eBookStore.Api.Dto.Response;

namespace Asm2.eBookStore.Client.Models;

public class BooksViewModel : IPagingResult
{
    public IList<BookDto> Books { get; set; } = new List<BookDto>();
    public string? Title { get; set; }
    public int? Price { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}
