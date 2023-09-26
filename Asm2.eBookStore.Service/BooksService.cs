using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Repository;

namespace Asm2.eBookStore.Service;

public class BooksService
{
    private readonly UnitOfWork _unitOfWork;

    public BooksService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Book> GetBook()
    {
        return _unitOfWork.Books.GetByOdata();
    }
}