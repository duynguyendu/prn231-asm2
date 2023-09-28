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

    public IQueryable<BookAuthor> GetBookAuthorsById(int id)
    {
        return _unitOfWork.BookAuthors.GetByOdata().Where(x => x.BookId == id);
    }

    public async Task<Book> Add(Book book, ICollection<int> authorIds)
    {
        var bookAuthors = authorIds
            .Select(x => new BookAuthor { AuthorId = x, Book = book })
            .ToList();
        book.BookAuthors = bookAuthors;
        _unitOfWork.Books.Add(book);
        await _unitOfWork.SaveAsync();
        return book;
    }

    public async Task AddAuthor(int bookId, int authorId)
    {
        if (_unitOfWork.BookAuthors.ExistByBookIdAndAuthorId(bookId, authorId))
        {
            return;
        }
        var bookAuthor = new BookAuthor { BookId = bookId, AuthorId = authorId };
        _unitOfWork.BookAuthors.Add(bookAuthor);
        await _unitOfWork.SaveAsync();
    }

    public async Task<Book> Add(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Book> Update(Book book)
    {
        _unitOfWork.Books.Update(book);
        await _unitOfWork.SaveAsync();
        return book;
    }

    public async Task DeleteById(int id)
    {
        _unitOfWork.BookAuthors.DeleteByBookId(id);
        _unitOfWork.Books.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteByBookIdAndAuthorId(int bookId, int authorId)
    {
        if (!_unitOfWork.BookAuthors.ExistByBookIdAndAuthorId(bookId, authorId))
        {
            return;
        }
        _unitOfWork.BookAuthors.DeleteByBookIdAndAuthorId(bookId, authorId);
        await _unitOfWork.SaveAsync();
    }
}
