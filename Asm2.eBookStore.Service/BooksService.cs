using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class BooksService
{
    private readonly EBookStoreDbContext context = new EBookStoreDbContext();

    public DbSet<Book> GetBook()
    {
        return context.Books;
    }
}