using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class Role
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }
    public string Description { get; set; } = null!;
}