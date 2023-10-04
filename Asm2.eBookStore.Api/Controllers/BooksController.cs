using Asm2.eBookStore.Api.Attributes;
using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Asm2.eBookStore.Api.Controllers;

[Authorize(Roles = "admin")]
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
        return Ok(
            SingleResult.Create(
                _booksService
                    .Get()
                    .Where(x => x.Id == key)
                    .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            )
        );
    }

    [EnableQuery]
    public ActionResult<IQueryable<BookAuthor>> GetBookAuthors([FromODataUri] int key)
    {
        return Ok(_booksService.GetBookAuthorsById(key));
    }

    [EnableQuery]
    public ActionResult<IQueryable<BookAuthor>> GetRefToBookAuthors([FromODataUri] int key)
    {
        return Ok(_booksService.GetBookAuthorsById(key));
    }

    [EnableQuery]
    public async Task<ActionResult> CreateRefToBookAuthors(
        [FromODataUri] int key,
        [FromBody] Uri link
    )
    {
        if (!TryParseRelatedKey(link, out var authorId))
        {
            return BadRequest();
        }
        await _booksService.AddAuthor(key, authorId);
        return NoContent();
    }

    [EnableQuery]
    public async Task<ActionResult> DeleteRefToBookAuthors(
        [FromODataUri] int key,
        [FromBody] Uri link
    )
    {
        if (!TryParseRelatedKey(link, out var authorId))
        {
            return BadRequest();
        }
        await _booksService.DeleteByBookIdAndAuthorId(key, authorId);
        return NoContent();
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
        book.Id = key;
        var createdBook = await _booksService.Update(book);
        return Ok(_mapper.Map<BookDto>(createdBook));
    }

    [EnableQuery]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _booksService.DeleteById(key);
        return NoContent();
    }

    private bool TryParseRelatedKey(Uri link, out int relatedKey)
    {
        relatedKey = 0;

        var model = Request.GetRouteServices().GetService(typeof(IEdmModel)) as IEdmModel;
        var serviceRoot = Request.CreateODataLink();

        var uriParser = new ODataUriParser(model, new Uri(serviceRoot), link);
        // NOTE: ParsePath may throw exceptions for various reasons
        var odataPath = uriParser.ParsePath();
        var keySegment = odataPath.OfType<KeySegment>().LastOrDefault();

        return keySegment != null
            && int.TryParse(keySegment.Keys.First().Value.ToString(), out relatedKey);
    }
}
