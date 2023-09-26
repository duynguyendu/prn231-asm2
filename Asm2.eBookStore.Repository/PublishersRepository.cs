using Asm2.eBookStore.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Repository;

public class PublishersRepository
{
   private readonly EBookStoreDbContext context;

   public PublishersRepository(EBookStoreDbContext context)
   {
      this.context = context;
   }

   public DbSet<Publisher> GetByOdata()
   {
      return context.Publishers;
   }

   public Publisher? GetById(int id)
   {
      return context.Publishers.SingleOrDefault(x => x.PublisherId == id);
   }

   public void Add(Publisher publisher)
   {
      context.Publishers.Add(publisher);
   }
    
   public void Update(Publisher publisher)
   {
      context.Publishers.Update(publisher);
   }

   public void Delete(int id)
   {
      var publisher = context.Publishers.SingleOrDefault(x => x.PublisherId == id);
      if (publisher != null)
      {
         context.Publishers.Remove(publisher);
      }
   }
}