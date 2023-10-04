using Asm2.eBookStore.Api.Attributes;
using Asm2.eBookStore.Api.Dto.Request;
using Asm2.eBookStore.Api.Dto.Response;
using Asm2.eBookStore.EntityModel;
using Asm2.eBookStore.Service;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asm2.eBookStore.Api.Controllers;

[Authorize(Roles = "admin")]
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
        return Ok(_publishersService.Get().ProjectTo<PublisherDto>(_mapper.ConfigurationProvider));
    }

    [EnableQuery]
    public ActionResult<SingleResult<PublisherDto>> Get([FromODataUri] int key)
    {
        return Ok(_mapper.Map<PublisherDto>(_publishersService.GetById(key)));
    }

    [EnableQuery]
    [ValidateModel]
    public async Task<ActionResult<PublisherDto>> Post([FromBody] PublisherUpdateDto updateDto)
    {
        var publisher = _mapper.Map<Publisher>(updateDto);
        var createdPublisher = await _publishersService.Add(publisher);
        return Created(_mapper.Map<PublisherDto>(createdPublisher));
    }

    [EnableQuery]
    [ValidateModel]
    public async Task<ActionResult<PublisherDto>> Put(
        [FromODataUri] int key,
        [FromBody] PublisherUpdateDto updateDto
    )
    {
        var publisher = _mapper.Map<Publisher>(updateDto);
        publisher.Id = key;
        var createdPublisher = await _publishersService.Update(publisher);
        return Ok(_mapper.Map<PublisherDto>(createdPublisher));
    }

    [EnableQuery]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        await _publishersService.DeleteById(key);
        return NoContent();
    }
}
