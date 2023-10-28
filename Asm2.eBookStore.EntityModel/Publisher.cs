using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asm2.eBookStore.EntityModel;

public class Publisher : GenericEntity
{
    public string Name { get; set; } = null!;
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
