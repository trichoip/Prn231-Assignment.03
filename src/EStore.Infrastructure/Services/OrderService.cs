using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Services;
public class OrderService : IOrderService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<OrderDto>> FindAllAsync()
    {
        var entity = await _unitOfWork.OrderRepository.FindAllAsync();
        return _mapper.Map<IList<OrderDto>>(entity);
    }

    public async Task<OrderDto?> FindByIdAsync(int id)
    {
        var entity = await _unitOfWork.OrderRepository.FindByIdAsync(id);
        return _mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> CreateAsync(OrderDto entityDto)
    {
        if (entityDto.MemberId != null)
            if (await _unitOfWork.Entities<ApplicationUser>().AllAsync(_ => _.Id != entityDto.MemberId))
                throw new KeyNotFoundException($"Entity with id {entityDto.MemberId} not found");
        var entity = _mapper.Map<Order>(entityDto);
        await _unitOfWork.OrderRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<OrderDto>(entity);
    }

    public async Task UpdateAsync(OrderDto entityDto)
    {
        if (entityDto.MemberId != null)
            if (await _unitOfWork.Entities<ApplicationUser>().AllAsync(_ => _.Id != entityDto.MemberId))
                throw new KeyNotFoundException($"Entity with id {entityDto.MemberId} not found");
        var entity = await _unitOfWork.OrderRepository.FindByIdAsync(entityDto.OrderId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.OrderId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.OrderRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.OrderRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.OrderRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
