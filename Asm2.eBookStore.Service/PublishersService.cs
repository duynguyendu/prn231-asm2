using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class PublishersService
{

    private readonly UnitOfWork _unitOfWork;

    public PublishersService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public DbSet<Publisher> GetPublisher()
    {
        return _unitOfWork.Publishers.GetByOdata();
    }

    public Publisher? GetPublisherById(int id)
    {
        return _unitOfWork.Publishers.GetById(id);
    } 

    public Publisher AddPublisher(Publisher publisher)
    {
        _unitOfWork.Publishers.Add(publisher);
        _unitOfWork.Save();
        return publisher;
    }
    public Publisher UpdatePublisher(Publisher publisher)
    {
        _unitOfWork.Publishers.Update(publisher);
        _unitOfWork.Save();
        return publisher;
    }
    public void DeletePublisherById(int id)
    {
        _unitOfWork.Publishers.Delete(id);
        _unitOfWork.Save();
    } 
}