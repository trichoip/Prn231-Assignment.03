using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EStore.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public IQueryable<User> Entities => _unitOfWork.UserRepository.Entities;

    public async Task<UserDto> CreateAsync(UserDto entityDto)
    {
        if (entityDto.PubId != null && entityDto.PubId != 0)
        {
            if (await _unitOfWork.PublisherRepository.FindByIdAsync(entityDto.PubId.GetValueOrDefault()) is null)
                throw new KeyNotFoundException($"Entity with id {entityDto.PubId} not found");
        }
        if (entityDto.RoleId != null && entityDto.RoleId != 0)
        {
            if (!await _unitOfWork.Entities<Role>().AnyAsync(r => r.RoleId == entityDto.RoleId))
                throw new KeyNotFoundException($"Entity with id {entityDto.RoleId} not found");
        }
        var entity = _mapper.Map<User>(entityDto);
        await _unitOfWork.UserRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<UserDto>(entity);
    }

    public async Task<User?> FindByIdAsync(int id) => await _unitOfWork.UserRepository.FindByIdAsync(id);

    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.UserRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.UserRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(UserDto entityDto)
    {
        if (entityDto.PubId != null && entityDto.PubId != 0)
        {
            if (await _unitOfWork.PublisherRepository.FindByIdAsync(entityDto.PubId.GetValueOrDefault()) is null)
                throw new KeyNotFoundException($"Entity with id {entityDto.PubId} not found");
        }
        if (entityDto.RoleId != null && entityDto.RoleId != 0)
        {
            if (!await _unitOfWork.Entities<Role>().AnyAsync(r => r.RoleId == entityDto.RoleId))
                throw new KeyNotFoundException($"Entity with id {entityDto.RoleId} not found");
        }
        var entity = await _unitOfWork.UserRepository.FindByIdAsync(entityDto.UserId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.UserId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.UserRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
