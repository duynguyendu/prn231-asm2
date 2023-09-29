using System.ComponentModel.DataAnnotations;

namespace Asm2.eBookStore.Api.Dto.Request;

public class BookUpdateDto
{
    // TODO: validation
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Advance { get; set; } = null!;
    public string? Royalty { get; set; } = null!;
    public decimal? YtdSales { get; set; } = null!;
    public string? Notes { get; set; } = null!;
    public DateTime? PublishedDate { get; set; } = null!;
}
