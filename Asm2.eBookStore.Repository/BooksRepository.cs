using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class BooksRepository : GenericRepository<Book>
{
    public BooksRepository(EBookStoreDbContext context) : base(context)
    {
    }
}