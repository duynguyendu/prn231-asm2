using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;

namespace Asm2.eBookStore.Service;

public class UsersService
{
    private readonly UnitOfWork _unitOfWork;

    public UsersService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public User? Login(string email, string password)
    {
        return _unitOfWork.Users.GetUserByEmailAndPassword(email, password);
    }
}