using System.ComponentModel.DataAnnotations;

namespace Asm2.eBookStore.Api.Dto.Request;

public class PublisherUpdateDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
}