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
public class AuthorsController : ODataController
{
    private readonly AuthorsService _authorsService;
    private readonly IMapper _mapper;

    public AuthorsController(AuthorsService authorsService, IMapper mapper)
    {
        _authorsService = authorsService;
        _mapper = mapper;
    }

    [EnableQuery(PageSize = 3)]
    public ActionResult<ICollection<AuthorDto>> Get()
    {
        return Ok(_authorsService.Get().ProjectTo<AuthorDto>(_mapper.ConfigurationProvider));
    }

    [EnableQuery]
    public ActionResult<SingleResult<AuthorDto>> Get([FromODataUri] int key)
    {
        return Ok(_mapper.Map<AuthorDto>(_authorsService.GetById(key)));
    }

    [EnableQuery]
    [ValidateModel]
    public async Task<ActionResult<AuthorDto>> Post([FromBody] AuthorUpdateDto updateDto)
    {
        var author = _mapper.Map<Author>(updateDto);
        var createdAuthor = await _authorsService.Add(author);
        return Created(_mapper.Map<AuthorDto>(createdAuthor));
    }

    [EnableQuery]
    [ValidateModel]
    public async Task<ActionResult<AuthorDto>> Put(
        [FromODataUri] int key,
        [FromBody] AuthorUpdateDto updateDto
    )
    {
        var author = _mapper.Map<Author>(updateDto);
        author.Id = key;
        var createdAuthor = await _authorsService.Update(author);
        return Ok(_mapper.Map<AuthorDto>(createdAuthor));
    }

    [EnableQuery]
    public async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var result = await _authorsService.DeleteById(key);
        if (result)
            return NoContent();
        return BadRequest();
    }
}
