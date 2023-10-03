using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Services;
public class ApplicationUserService : IApplicationUserService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ApplicationUserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IList<ApplicationUserDto>> FindAllAsync()
    {
        var entity = await _unitOfWork.ApplicationUserRepository.FindAllAsync();
        return _mapper.Map<IList<ApplicationUserDto>>(entity);
    }

    public async Task<ApplicationUserDto?> FindByIdAsync(int id)
    {
        var entity = await _unitOfWork.ApplicationUserRepository.FindByIdAsync(id);
        return _mapper.Map<ApplicationUserDto>(entity);
    }

    public async Task<ApplicationUserDto> CreateAsync(ApplicationUserDto entityDto)
    {
        var entity = _mapper.Map<ApplicationUser>(entityDto);
        await _unitOfWork.ApplicationUserRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<ApplicationUserDto>(entity);
    }

    public async Task UpdateAsync(ApplicationUserDto entityDto)
    {
        var entity = await _unitOfWork.ApplicationUserRepository.FindByIdAsync(entityDto.Id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.Id} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.ApplicationUserRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.ApplicationUserRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.ApplicationUserRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
