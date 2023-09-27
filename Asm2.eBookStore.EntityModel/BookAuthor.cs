using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.EntityModel;

public class BookAuthor : GenericEntity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookAuthorId { get; set; }

    [ForeignKey(nameof(Book))]
    public int? BookId { get; set; }

    [ForeignKey(nameof(Author))]
    public int? AuthorId { get; set; }

    public int AuthorOrder { get; set; }
    public decimal? RoyaltyPercentage { get; set; }

    public virtual Book? Book { get; set; }
    public virtual Author? Author { get; set; }

    public override int Id => BookAuthorId;
}
