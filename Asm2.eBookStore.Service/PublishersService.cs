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

    public async Task<Publisher> Add(Publisher publisher)
    {
        _unitOfWork.Publishers.Add(publisher);
        await _unitOfWork.SaveAsync();
        return publisher;
    }

    public async Task<Publisher> Update(Publisher publisher)
    {
        _unitOfWork.Publishers.Update(publisher);
        await _unitOfWork.SaveAsync();
        return publisher;
    }

    public async Task DeleteById(int id)
    {
        // TODO: check for publisher and book
        _unitOfWork.Publishers.Delete(id);
        await _unitOfWork.SaveAsync();
    }
}
