using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class User : GenericEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Source { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MiddleName { get; set; }
    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }
    [ForeignKey(nameof(Publisher))]
    public int PublisherId { get; set; }
    public DateTime HireDate { get; set; }
    
    public virtual Role? Role { get; set; }
    public virtual Publisher? Publisher { get; set; }

    public override int Id => UserId;
}
