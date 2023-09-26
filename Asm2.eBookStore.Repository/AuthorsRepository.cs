using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class AuthorsRepository : GenericRepository<Author>
{
    public AuthorsRepository(EBookStoreDbContext context) : base(context)
    {
    }
}