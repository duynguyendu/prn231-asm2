using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class UsersRepository
{
   private readonly EBookStoreDbContext context;

   public UsersRepository(EBookStoreDbContext context)
   {
      this.context = context;
   }
   
    public User? GetUserByEmailAndPassword(string email, string password)
    {
        return context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
    }
}