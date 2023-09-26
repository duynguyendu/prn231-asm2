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

    public async Task<Book> Add(Book book)
    {
        // TODO: create book author
        _unitOfWork.Books.Add(book);
        await _unitOfWork.SaveAsync();
        return book;
    }

    public async Task<Book> Update(Book book)
    {
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveAsync();
        return book;
    }

    public async Task DeleteById(int id)
    {
        _unitOfWork.Books.Delete(id);
        await _unitOfWork.SaveAsync();
    }
}
