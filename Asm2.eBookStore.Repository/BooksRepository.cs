using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class BooksRepository
{
   private readonly EBookStoreDbContext context;

   public BooksRepository(EBookStoreDbContext context)
   {
      this.context = context;
   }

   public IQueryable<Book> GetByOdata()
   {
      return context.Books;
   }
}