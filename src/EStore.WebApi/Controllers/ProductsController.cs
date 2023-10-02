using EStore.Application.DTOs;
using EStore.Application.Services;
using EStore.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _entityService;
    private readonly UserManager<ApplicationUser> userManager;

    public ProductsController(IProductService entityService, UserManager<ApplicationUser> _userManager)
    {
        _entityService = entityService;
        userManager = _userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var a = userManager.Users.Include(c => c.Orders).ToList();

        return Ok(a);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var entity = await _entityService.FindByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPut]
    public async Task<IActionResult> PutProduct(int id, [FromBody] ProductDto entityDto)
    {
        if (id != entityDto.ProductId) return BadRequest();
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

    [HttpPost]
    public async Task<IActionResult> PostProduct([FromBody] ProductDto entityDto)
    {
        if (entityDto.ProductId != 0) return BadRequest();
        entityDto = await _entityService.CreateAsync(entityDto);
        return CreatedAtAction(nameof(GetProduct), new { id = entityDto.ProductId }, entityDto);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
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
