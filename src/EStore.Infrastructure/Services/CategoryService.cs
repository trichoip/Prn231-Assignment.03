using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<CategoryDto>> FindAllAsync()
    {
        var entity = await _unitOfWork.CategoryRepository.FindAllAsync();
        return _mapper.Map<IList<CategoryDto>>(entity);
    }

    public async Task<CategoryDto?> FindByIdAsync(int id)
    {
        var entity = await _unitOfWork.CategoryRepository.FindByIdAsync(id);
        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto entityDto)
    {
        var entity = _mapper.Map<Category>(entityDto);
        await _unitOfWork.CategoryRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<CategoryDto>(entity);
    }

    public async Task UpdateAsync(CategoryDto entityDto)
    {
        var entity = await _unitOfWork.CategoryRepository.FindByIdAsync(entityDto.CategoryId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.CategoryId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.CategoryRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.CategoryRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.CategoryRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
