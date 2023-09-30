using EStore.Application.DTOs;
using EStore.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace EStore.WebApi.Controllers;

public class BooksController : ODataController
{
    private readonly IBookService _entityService;

    public BooksController(IBookService entityService)
    {
        _entityService = entityService;
    }

    [EnableQuery]
    public async Task<IActionResult> Get() => Ok(_entityService.Entities);

    [EnableQuery]
    public async Task<IActionResult> Get(int key)
    {
        var entity = await _entityService.Entities.Include(c => c.Publisher).FirstOrDefaultAsync(c => c.BookId == key);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    public async Task<IActionResult> Put(int key, [FromBody] BookDto entityDto)
    {
        if (key != entityDto.BookId) return BadRequest();
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

    public async Task<IActionResult> Post([FromBody] BookDto entityDto)
    {
        if (entityDto.BookId != 0) return BadRequest();
        try
        {
            entityDto = await _entityService.CreateAsync(entityDto);
        }
        catch (KeyNotFoundException ex)
        {
            return Problem(statusCode: 404, detail: ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(statusCode: 500, detail: ex.Message);
        }

        return CreatedAtAction(nameof(Get), new { key = entityDto.BookId }, entityDto);
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