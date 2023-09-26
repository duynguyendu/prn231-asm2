using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Repository;

public class PublishersRepository : GenericRepository<Publisher>
{
    public PublishersRepository(EBookStoreDbContext context) : base(context)
    {
    }
}