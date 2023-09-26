using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class Publisher : GenericEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PublisherId { get; set; }
    public string Name { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    public override int Id => PublisherId;
}