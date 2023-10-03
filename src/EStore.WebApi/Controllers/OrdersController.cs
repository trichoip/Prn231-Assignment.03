using EStore.Application.DTOs;
using EStore.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{

    private readonly IOrderService _entityService;

    public OrdersController(IOrderService entityService)
    {
        _entityService = entityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        return Ok(await _entityService.FindAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var entity = await _entityService.FindByIdAsync(id);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> PostOrder(OrderDto entityDto)
    {
        if (entityDto.OrderId != 0) return BadRequest();
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
        return CreatedAtAction(nameof(GetOrder), new { id = entityDto.OrderId }, entityDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, OrderDto entityDto)
    {
        if (id != entityDto.OrderId) return BadRequest();
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
    public async Task<IActionResult> DeleteOrder(int id)
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
