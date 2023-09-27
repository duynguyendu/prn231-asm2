using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class Role : GenericEntity
{
    public string Description { get; set; } = null!;
}
