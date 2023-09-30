using Asm2.eBookStore.Api.Dto.Response;

namespace Asm2.eBookStore.Client.Models.Publishers;

public class PublishersViewModel : IPagingResult
{
    public IList<PublisherDto> Publishers { get; set; } = new List<PublisherDto>();
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int TotalCount { get; set; }
}
