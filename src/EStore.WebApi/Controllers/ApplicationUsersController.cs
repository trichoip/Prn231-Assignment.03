using EStore.Application.DTOs;
using EStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EStore.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ApplicationUsersController : ControllerBase
{
    private readonly IApplicationUserService _entityService;

    public ApplicationUsersController(IApplicationUserService entityService)
    {
        _entityService = entityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplicationUsers()
    {
        return Ok(await _entityService.FindAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationUser(int id)
    {
        var entity = await _entityService.FindByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> PostApplicationUser(ApplicationUserDto entityDto)
    {
        if (entityDto.Id != 0) return BadRequest();
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
        return CreatedAtAction(nameof(GetApplicationUser), new { id = entityDto.Id }, entityDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutApplicationUser(int id, ApplicationUserDto entityDto)
    {
        if (id != entityDto.Id) return BadRequest();
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplicationUser(int id)
    {
        try
        {
            await _entityService.RemoveAsync(id);
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
