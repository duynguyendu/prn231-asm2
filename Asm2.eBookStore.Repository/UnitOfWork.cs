using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class UnitOfWork : IDisposable
{
    private readonly EBookStoreDbContext context = new();
    private BooksRepository? _booksRepository;
    public BooksRepository Books => _booksRepository ??= new BooksRepository(context);
    private PublishersRepository? _publishersRepository;
    public PublishersRepository Publishers => _publishersRepository ??= new PublishersRepository(context);
    
    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            context.Dispose();
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}