using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class PublishersService : IGenericService<Publisher>
{

    private readonly UnitOfWork _unitOfWork;

    public PublishersService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public DbSet<Publisher> Get()
    {
        return _unitOfWork.Publishers.GetByOdata();
    }

    public Publisher? GetById(int id)
    {
        return _unitOfWork.Publishers.GetById(id);
    } 

    public Publisher Add(Publisher author)
    {
        _unitOfWork.Publishers.Add(author);
        _unitOfWork.Save();
        return author;
    }
    public Publisher Update(Publisher publisher)
    {
        _unitOfWork.Publishers.Update(publisher);
        _unitOfWork.Save();
        return publisher;
    }
    public void DeleteById(int id)
    {
        _unitOfWork.Publishers.Delete(id);
        _unitOfWork.Save();
    } 
}