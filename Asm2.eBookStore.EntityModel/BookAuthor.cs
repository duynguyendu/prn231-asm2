using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.EntityModel;

[PrimaryKey(nameof(BookId), nameof(AuthorId))]
public class BookAuthor
{
    public int? BookId { get; set; }
    [ForeignKey(nameof(Book))]
    public int? AuthorId { get; set; }
    [ForeignKey(nameof(Author))]
    public int AuthorOrder { get; set; }
    public decimal? RoyaltyPercentage { get; set; }
    
    public virtual Book? Book { get; set; }
    public virtual Author? Author { get; set; }
}