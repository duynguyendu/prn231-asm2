using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class BookAuthorsRepository : GenericRepository<BookAuthor>
{
    public BookAuthorsRepository(EBookStoreDbContext context)
        : base(context) { }

    public void DeleteByBookId(int bookId)
    {
        var bookAuthors = context.BookAuthors.Where(x => x.BookId == bookId);
        context.BookAuthors.RemoveRange(bookAuthors);
    }

    public bool ExistByAuthorId(int authorId)
    {
        return context.BookAuthors.Any(x => x.AuthorId == authorId);
    }
}
