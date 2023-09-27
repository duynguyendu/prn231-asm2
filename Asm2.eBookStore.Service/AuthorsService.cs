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

    public async Task<Author> Add(Author author)
    {
        _unitOfWork.Authors.Add(author);
        await _unitOfWork.SaveAsync();
        return author;
    }

    public async Task<Author> Update(Author author)
    {
        _unitOfWork.Authors.Update(author);
        await _unitOfWork.SaveAsync();
        return author;
    }

    public async Task DeleteById(int id)
    {
        if (!_unitOfWork.BookAuthors.ExistByAuthorId(id))
        {
            _unitOfWork.Authors.Delete(id);
            // TODO: only delete if there is no BookAuthor
            await _unitOfWork.SaveAsync();
        }
    }
}
