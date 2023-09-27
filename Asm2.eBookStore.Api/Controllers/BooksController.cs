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
    public async Task<ActionResult<BookDto>> Post([FromBody] BookCreateDto createDto)
    {
        var book = _mapper.Map<Book>(createDto);
        var createdBook = await _booksService.Add(book, createDto.AuthorIds);
        return Created(_mapper.Map<BookDto>(createdBook));
    }

    [EnableQuery]
    [ValidateModel]
    public async Task<ActionResult<BookDto>> Put(
        [FromODataUri] int key,
        [FromBody] BookUpdateDto updateDto
    )
    {
        var book = _mapper.Map<Book>(updateDto);
        book.BookId = key;
        var createdBook = await _booksService.Update(book);
        return Ok(_mapper.Map<BookDto>(createdBook));
    }

    [EnableQuery]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _booksService.DeleteById(key);
        return NoContent();
    }
}
