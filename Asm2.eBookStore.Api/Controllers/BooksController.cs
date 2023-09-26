using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asm2.eBookStore.Api.Controllers;

public class BooksController : ODataController
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    [EnableQuery]
    public ActionResult<IQueryable<Book>> Get()
    {
        return Ok(_booksService.GetBook());
    }
}