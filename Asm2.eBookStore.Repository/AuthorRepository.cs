using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class AuthorRepository : GenericRepository<Author>
{
    public AuthorRepository(EBookStoreDbContext context) : base(context)
    {
    }
}