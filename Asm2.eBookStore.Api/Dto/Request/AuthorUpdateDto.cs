using System.ComponentModel.DataAnnotations;

namespace Asm2.eBookStore.Api.Dto.Request;

public class AuthorUpdateDto
{
    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? State { get; set; } = null!;
    public string? Zip { get; set; } = null!;
    public string? Email { get; set; } = null!;
}
