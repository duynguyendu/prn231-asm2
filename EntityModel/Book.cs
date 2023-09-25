using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel;

public class Book
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookId { get; set; }
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
    
    public virtual Publisher? Publisher { get; set; }
}