using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EStore.Infrastructure.Services;
public class OrderDetailService : IOrderDetailService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public OrderDetailService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<OrderDetailDto>> FindAllAsync(Expression<Func<OrderDetail, bool>>? expression = null)
    {
        var entity = await _unitOfWork.OrderDetailRepository.FindAllAsync(expression);
        return _mapper.Map<IList<OrderDetailDto>>(entity);
    }

    public async Task<OrderDetailDto?> FindByIdAsync(object?[] ids)
    {
        var entity = await _unitOfWork.OrderDetailRepository.FindByIdAsync(ids);
        return _mapper.Map<OrderDetailDto>(entity);
    }

    public async Task<OrderDetailDto> CreateAsync(OrderDetailDto entityDto)
    {
        if (await _unitOfWork.ProductRepository.Entities.AllAsync(_ => _.ProductId != entityDto.ProductId))
            throw new KeyNotFoundException("Product not found");
        if (await _unitOfWork.OrderRepository.Entities.AllAsync(_ => _.OrderId != entityDto.OrderId))
            throw new KeyNotFoundException("Order not found");
        if (await _unitOfWork.OrderDetailRepository.Entities
                   .AnyAsync(_ => _.ProductId == entityDto.ProductId && _.OrderId == entityDto.OrderId))
            throw new Exception("OrderDetail is exist");

        var entity = _mapper.Map<OrderDetail>(entityDto);
        await _unitOfWork.OrderDetailRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<OrderDetailDto>(entity);
    }

    public async Task RemoveAsync(Expression<Func<OrderDetail, bool>> expression)
    {
        var entity = await _unitOfWork.OrderDetailRepository.FindAllAsync(expression);
        if (entity.Count == 0) throw new KeyNotFoundException("OrderDetail not found");
        await _unitOfWork.OrderDetailRepository.RemoveRangeAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(OrderDetailDto entityDto)
    {
        var entity = await _unitOfWork.OrderDetailRepository.FindByIdAsync(new object?[] { entityDto.ProductId, entityDto.OrderId });
        if (entity == null) throw new KeyNotFoundException("OrderDetail not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.OrderDetailRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
