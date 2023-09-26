using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class AuthorsService : IGenericService<Author>
{
    private readonly UnitOfWork _unitOfWork;

    public AuthorsService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public DbSet<Author> Get()
    {
        return _unitOfWork.Authors.GetByOdata();
    }

    public Author? GetById(int id)
    {
        return _unitOfWork.Authors.GetById(id);
    }

    public Author Add(Author author)
    {
        _unitOfWork.Authors.Add(author);
        _unitOfWork.Save();
        return author;
    }

    public Author Update(Author author)
    {
        _unitOfWork.Authors.Update(author);
        _unitOfWork.Save();
        return author;
    }

    public void DeleteById(int id)
    {
        _unitOfWork.Authors.Delete(id);
        _unitOfWork.Save();
    }
}