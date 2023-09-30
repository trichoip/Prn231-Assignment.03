using EStore.Application.DTOs;
using EStore.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace EStore.WebApi.Controllers;

public class AuthorsController : ODataController
{
    private readonly IAuthorService _entityService;

    public AuthorsController(IAuthorService entityService)
    {
        _entityService = entityService;
    }

    [EnableQuery]
    public async Task<IActionResult> Get() => Ok(_entityService.Entities);

    [EnableQuery]
    public async Task<IActionResult> Get(int key)
    {
        var entity = await _entityService.FindByIdAsync(key);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    public async Task<IActionResult> Put(int key, [FromBody] AuthorDto entityDto)
    {
        if (key != entityDto.AuthorId) return BadRequest();
        try
        {
            await _entityService.UpdateAsync(entityDto);
        }
        catch (KeyNotFoundException ex)
        {
            return Problem(statusCode: 404, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }

        return NoContent();
    }

    public async Task<IActionResult> Post([FromBody] AuthorDto entityDto)
    {
        if (entityDto.AuthorId != 0) return BadRequest();
        entityDto = await _entityService.CreateAsync(entityDto);
        return CreatedAtAction(nameof(Get), new { key = entityDto.AuthorId }, entityDto);
    }

    public async Task<IActionResult> Delete(int key)
    {
        try
        {
            await _entityService.RemoveAsync(key);
        }
        catch (KeyNotFoundException ex)
        {
            return Problem(statusCode: 404, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }

        return NoContent();
    }

}
