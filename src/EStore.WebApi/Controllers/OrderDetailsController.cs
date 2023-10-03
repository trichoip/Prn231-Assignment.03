using EStore.Application.DTOs;
using EStore.Application.Services;
using EStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace EStore.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase
{

    private readonly IOrderDetailService _entityService;

    public OrderDetailsController(IOrderDetailService entityService)
    {
        _entityService = entityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderDetails(int orderId, int productId)
    {
        Expression<Func<OrderDetail, bool>>? expression = orderId == 0 && productId == 0 ? null :
            orderId != 0 && productId == 0 ? x => x.OrderId == orderId :
            orderId == 0 && productId != 0 ? x => x.ProductId == productId :
            x => x.OrderId == orderId && x.ProductId == productId;

        return Ok(await _entityService.FindAllAsync(expression));
    }

    [HttpGet("{orderId}/{productId}")]
    public async Task<IActionResult> GetOrderDetail(int orderId, int productId)
    {
        var entity = await _entityService.FindByIdAsync(new object?[] { productId, orderId });
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> PostOrderDetail(OrderDetailDto entityDto)
    {
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
        return Ok(entityDto);
    }

    [HttpPut]
    public async Task<IActionResult> PutOrderDetail(OrderDetailDto entityDto)
    {
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

    [HttpDelete]
    public async Task<IActionResult> DeleteOrderDetail(int orderId, int productId)
    {
        Expression<Func<OrderDetail, bool>>? expression = orderId == 0 && productId == 0 ? null :
            orderId != 0 && productId == 0 ? x => x.OrderId == orderId :
            orderId == 0 && productId != 0 ? x => x.ProductId == productId :
            x => x.OrderId == orderId && x.ProductId == productId;

        if (expression == null) return BadRequest();
        try
        {
            await _entityService.RemoveAsync(expression);
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
