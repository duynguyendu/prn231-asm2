using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel;

public class Author
{
   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
   public int AuthorId { get; set; }
   public string LastName { get; set; } = null!;
   public string FirstName { get; set; } = null!;
   public string? Phone { get; set; } = null!;
   public string? Address { get; set; } = null!;
   public string? City { get; set; } = null!;
   public string? State { get; set; } = null!;
   public string? Zip { get; set; } = null!;
   public string? Email { get; set; } = null!;
}