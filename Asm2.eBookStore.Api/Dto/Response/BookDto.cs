using System.ComponentModel.DataAnnotations;
using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Api.Dto.Response;

public class BookDto
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int? PublisherId { get; set; }
    public decimal Price { get; set; }
    public string? Advance { get; set; } = null!;
    public string? Royalty { get; set; } = null!;
    public decimal? YtdSales { get; set; } = null!;
    public string? Notes { get; set; } = null!;
    public DateTime? PublishedDate { get; set; } = null!;
    public virtual PublisherDto? Publisher { get; set; }
    public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
}
