using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Services;
public class ProductService : IProductService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<ProductDto>> FindAllAsync()
    {
        var entity = await _unitOfWork.ProductRepository.FindAllAsync();
        return _mapper.Map<IList<ProductDto>>(entity);
    }

    public async Task<ProductDto?> FindByIdAsync(int id)
    {
        var entity = await _unitOfWork.ProductRepository.FindByIdAsync(id);
        return _mapper.Map<ProductDto>(entity);
    }

    public async Task<ProductDto> CreateAsync(ProductDto entityDto)
    {
        if (entityDto.CategoryId != null)
            if (await _unitOfWork.CategoryRepository.Entities.AllAsync(_ => _.CategoryId != entityDto.CategoryId))
                throw new KeyNotFoundException($"Entity with id {entityDto.CategoryId} not found");
        var entity = _mapper.Map<Product>(entityDto);
        await _unitOfWork.ProductRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ProductDto>(entity);
    }

    public async Task UpdateAsync(ProductDto entityDto)
    {
        if (entityDto.CategoryId != null)
            if (await _unitOfWork.CategoryRepository.Entities.AllAsync(_ => _.CategoryId != entityDto.CategoryId))
                throw new KeyNotFoundException($"Entity with id {entityDto.CategoryId} not found");
        var entity = await _unitOfWork.ProductRepository.FindByIdAsync(entityDto.ProductId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.ProductId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.ProductRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.ProductRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.ProductRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
