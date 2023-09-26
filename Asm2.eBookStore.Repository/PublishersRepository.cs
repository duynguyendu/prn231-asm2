using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class PublishersRepository
{
   private readonly EBookStoreDbContext context;

   public PublishersRepository(EBookStoreDbContext context)
   {
      this.context = context;
   }

   public IQueryable<Publisher> GetByOdata()
   {
      return context.Publishers;
   }
}