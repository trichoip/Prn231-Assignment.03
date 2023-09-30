using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Services;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AuthorService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Author> Entities => _unitOfWork.AuthorRepository.Entities;

    public async Task<AuthorDto> CreateAsync(AuthorDto entityDto)
    {
        var entity = _mapper.Map<Author>(entityDto);
        await _unitOfWork.AuthorRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        _mapper.Map(entity, entityDto);
        return entityDto;
    }

    public async Task<Author?> FindByIdAsync(int id) => await _unitOfWork.AuthorRepository.FindByIdAsync(id);

    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.AuthorRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.AuthorRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(AuthorDto entityDto)
    {
        var entity = await _unitOfWork.AuthorRepository.FindByIdAsync(entityDto.AuthorId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.AuthorId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.AuthorRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
