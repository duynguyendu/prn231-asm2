using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class Book : GenericEntity
{
    public string Title { get; set; } = null!;
    public string Type { get; set; } = null!;

    [ForeignKey(nameof(Publisher))]
    public int? PublisherId { get; set; }
    public decimal Price { get; set; }
    public string? Advance { get; set; } = null!;
    public string? Royalty { get; set; } = null!;
    public decimal? YtdSales { get; set; } = null!;
    public string? Notes { get; set; } = null!;
    public DateTime? PublishedDate { get; set; } = null!;

    public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    public virtual Publisher? Publisher { get; set; }
}
