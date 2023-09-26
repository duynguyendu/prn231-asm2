using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;

namespace Asm2.eBookStore.Service;

public class PublishersService
{

    private readonly UnitOfWork _unitOfWork;

    public PublishersService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Publisher> GetPublisher()
    {
        return _unitOfWork.Publishers.GetByOdata();
    }
}