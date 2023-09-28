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

    public void DeleteByBookIdAndAuthorId(int bookId, int authorId)
    {
        var bookAuthors = context.BookAuthors.Where(
            x => x.BookId == bookId && x.AuthorId == authorId
        );
        context.BookAuthors.RemoveRange(bookAuthors);
    }

    public bool ExistByAuthorId(int authorId)
    {
        return context.BookAuthors.Any(x => x.AuthorId == authorId);
    }

    public bool ExistByBookIdAndAuthorId(int bookId, int authorId)
    {
        return context.BookAuthors.Any(x => x.AuthorId == authorId && x.BookId == bookId);
    }
}
