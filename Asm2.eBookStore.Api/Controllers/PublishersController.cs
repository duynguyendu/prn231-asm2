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
    public IActionResult Get()
    {
        return Ok(_publishersService.GetPublisher().AsQueryable());
    }
}