using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessModel;

public class Role
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RoleId { get; set; }
    public string Description { get; set; } = null!;
}