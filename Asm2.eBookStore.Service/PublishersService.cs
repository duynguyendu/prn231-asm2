using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class PublishersService
{
    
    private readonly EBookStoreDbContext context = new EBookStoreDbContext();

    public DbSet<Publisher> GetPublisher()
    {
        return context.Publishers;
    }
}