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

public class PublishersController : ODataController
{
    private readonly PublishersService _publishersService;
    private readonly IMapper _mapper;

    public PublishersController(PublishersService publishersService, IMapper mapper)
    {
        _publishersService = publishersService;
        _mapper = mapper;
    }

    [EnableQuery(PageSize = 3)]
    public ActionResult<ICollection<PublisherDto>> Get()
    {
        return Ok(_publishersService.GetPublisher().ProjectTo<PublisherDto>(_mapper.ConfigurationProvider));
    }
    
    [EnableQuery]
    public ActionResult<SingleResult<PublisherDto>> Get([FromODataUri] int key)
    {
        return Ok(_mapper.Map<PublisherDto>(_publishersService.GetPublisherById(key)));
    }

    [EnableQuery]
    [ValidateModel]
    public ActionResult<Publisher> Post([FromBody] PublisherUpdateDto updateDto)
    {
        var publisher = _mapper.Map<Publisher>(updateDto);
        var createdPublisher = _publishersService.AddPublisher(publisher);
        return Created(createdPublisher);
    }
    
    [EnableQuery]
    [ValidateModel]
    public ActionResult<Publisher> Put([FromODataUri] int key, [FromBody] PublisherUpdateDto updateDto)
    {
        var publisher = _mapper.Map<Publisher>(updateDto);
        publisher.PublisherId = key;
        var createdPublisher = _publishersService.UpdatePublisher(publisher);
        return Ok(createdPublisher);
    }
    
    [EnableQuery]
    public IActionResult Delete([FromODataUri] int key)
    {
        _publishersService.DeletePublisherById(key);
        return NoContent();
    }
}