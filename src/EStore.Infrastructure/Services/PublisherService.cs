using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Services;
public class PublisherService : IPublisherService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public PublisherService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Publisher> Entities => _unitOfWork.PublisherRepository.Entities;

    public async Task<PublisherDto> CreateAsync(PublisherDto entityDto)
    {
        var entity = _mapper.Map<Publisher>(entityDto);
        await _unitOfWork.PublisherRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<PublisherDto>(entity);
    }

    public async Task<Publisher?> FindByIdAsync(int id) => await _unitOfWork.PublisherRepository.FindByIdAsync(id);

    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.PublisherRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.PublisherRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(PublisherDto entityDto)
    {
        var entity = await _unitOfWork.PublisherRepository.FindByIdAsync(entityDto.PubId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.PubId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.PublisherRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
