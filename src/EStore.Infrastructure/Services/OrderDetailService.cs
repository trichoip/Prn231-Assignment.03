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

    public async Task CreateAsync(OrderDetailDto entityDto)
    {
        if (await _unitOfWork.ProductRepository.Entities.AllAsync(_ => _.ProductId != entityDto.ProductId))
            throw new Exception("Product not found");
        if (await _unitOfWork.OrderRepository.Entities.AllAsync(_ => _.OrderId != entityDto.OrderId))
            throw new Exception("Order not found");
        if (await _unitOfWork.OrderDetailRepository.Entities
                   .AnyAsync(_ => _.ProductId == entityDto.ProductId && _.OrderId == entityDto.OrderId))
            throw new Exception("OrderDetail is exist");

        var entity = _mapper.Map<OrderDetail>(entityDto);
        await _unitOfWork.OrderDetailRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IList<OrderDetailDto>> FindAllAsync(Expression<Func<OrderDetail, bool>>? expression = null)
    {
        var entity = _unitOfWork.OrderDetailRepository.Entities;
        if (expression != null) entity = entity.Where(expression);
        return await _mapper.ProjectTo<OrderDetailDto>(entity).ToListAsync();
    }

    public async Task<OrderDetailDto?> FindByIdAsync(Expression<Func<OrderDetail, bool>> expression)
    {
        var entity = _unitOfWork.OrderDetailRepository.Entities;
        if (expression != null) entity = entity.Where(expression);
        return _mapper.Map<OrderDetailDto>(await entity.FirstOrDefaultAsync());
    }

    public async Task RemoveAsync(Expression<Func<OrderDetail, bool>> expression)
    {
        var entity = _unitOfWork.OrderDetailRepository.Entities.Where(expression);

        await _unitOfWork.OrderDetailRepository.RemoveRangeAsync(await entity.ToListAsync());

    }

    public async Task UpdateAsync(OrderDetailDto entityDto)
    {
        var entity = await _unitOfWork.OrderDetailRepository.Entities
                    .Where(_ => _.ProductId == entityDto.ProductId && _.OrderId == entityDto.OrderId).FirstOrDefaultAsync();
        if (entity == null) throw new Exception("OrderDetail not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.OrderDetailRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
