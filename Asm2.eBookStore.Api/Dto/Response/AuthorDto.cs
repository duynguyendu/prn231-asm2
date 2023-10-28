using System.ComponentModel.DataAnnotations;
using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Api.Dto.Response;

public class AuthorDto : IODataEntity
{
    public static string EntitySet => "Authors";

    [Key]
    public int Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? Phone { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public string? City { get; set; } = null!;
    public string? State { get; set; } = null!;
    public string? Zip { get; set; } = null!;
    public string? Email { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";
    public virtual ICollection<BookAuthor>? BookAuthors { get; set; }
}
