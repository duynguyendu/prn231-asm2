using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asm2.eBookStore.Api.Controllers;

public class PublishersController : ODataController
{
    private readonly PublishersService _publishersService;

    public PublishersController(PublishersService publishersService)
    {
        _publishersService = publishersService;
    }

    [EnableQuery]
    public ActionResult<IQueryable<Publisher>> Get()
    {
        return Ok(_publishersService.GetPublisher().AsQueryable());
    }
}