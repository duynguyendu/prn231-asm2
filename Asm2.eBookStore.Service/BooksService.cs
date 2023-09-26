using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Asm2.eBookStore.Service;

public class BooksService : IGenericService<Book>
{
    private readonly UnitOfWork _unitOfWork;

    public BooksService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public DbSet<Book> Get()
    {
        return _unitOfWork.Books.GetByOdata();
    }

    public Book? GetById(int id)
    {
        return _unitOfWork.Books.GetById(id);
    }

    public Book Add(Book book)
    {
        _unitOfWork.Books.Add(book);
        _unitOfWork.Save();
        return book;
    }

    public Book Update(Book book)
    {
        _unitOfWork.Books.Update(book);
        _unitOfWork.Save();
        return book;
    }

    public void DeleteById(int id)
    {
        _unitOfWork.Books.Delete(id);
        _unitOfWork.Save();
    }
}
