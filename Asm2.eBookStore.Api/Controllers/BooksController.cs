using Asm2.eBookStore.Api.Attributes;
using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asm2.eBookStore.Api.Controllers;

public class BooksController : ODataController
{
    private readonly BooksService _booksService;
    private readonly IMapper _mapper;

    public BooksController(BooksService booksService, IMapper mapper)
    {
        _booksService = booksService;
        _mapper = mapper;
    }

    [EnableQuery(PageSize = 3)]
    public ActionResult<ICollection<BookDto>> Get()
    {
        return Ok(_booksService.Get().ProjectTo<BookDto>(_mapper.ConfigurationProvider));
    }

    [EnableQuery]
    public ActionResult<SingleResult<BookDto>> Get([FromODataUri] int key)
    {
        return Ok(_mapper.Map<BookDto>(_booksService.GetById(key)));
    }

    [EnableQuery]
    [ValidateModel]
    public ActionResult<BookDto> Post([FromBody] BookUpdateDto updateDto)
    {
        var book = _mapper.Map<Book>(updateDto);
        var createdBook = _booksService.Add(book);
        return Created(_mapper.Map<BookDto>(createdBook));
    }

    [EnableQuery]
    [ValidateModel]
    public ActionResult<BookDto> Put([FromODataUri] int key, [FromBody] BookUpdateDto updateDto)
    {
        var book = _mapper.Map<Book>(updateDto);
        book.BookId = key;
        var createdBook = _booksService.Update(book);
        return Ok(_mapper.Map<BookDto>(createdBook));
    }

    [EnableQuery]
    public IActionResult Delete([FromODataUri] int key)
    {
        _booksService.DeleteById(key);
        return NoContent();
    }
}
