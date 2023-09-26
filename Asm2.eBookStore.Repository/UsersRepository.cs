using Asm2.eBookStore.EntityModel;

namespace Asm2.eBookStore.Repository;

public class UsersRepository : GenericRepository<User>
{
    public UsersRepository(EBookStoreDbContext context) : base(context)
    {
    }

    public User? GetUserByEmailAndPassword(string email, string password)
    {
        return context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
    }
}