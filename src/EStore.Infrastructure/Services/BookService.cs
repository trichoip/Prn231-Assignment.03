using AutoMapper;
using EStore.Application.DTOs;
using EStore.Application.Repositories;
using EStore.Application.Services;
using EStore.Domain.Entities;

namespace EStore.Infrastructure.Services;
public class BookService : IBookService
{

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public BookService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public IQueryable<Book> Entities => _unitOfWork.BookRepository.Entities;

    public async Task<BookDto> CreateAsync(BookDto entityDto)
    {
        if (entityDto.PubId != null && entityDto.PubId != 0)
        {
            if (await _unitOfWork.PublisherRepository.FindByIdAsync(entityDto.PubId.GetValueOrDefault()) is null)
                throw new KeyNotFoundException($"Entity with id {entityDto.PubId} not found");
        }
        var entity = _mapper.Map<Book>(entityDto);
        await _unitOfWork.BookRepository.CreateAsync(entity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task<Book?> FindByIdAsync(int id) => await _unitOfWork.BookRepository.FindByIdAsync(id);

    public async Task RemoveAsync(int id)
    {
        var entity = await _unitOfWork.BookRepository.FindByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {id} not found");
        await _unitOfWork.BookRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(BookDto entityDto)
    {
        if (entityDto.PubId != null && entityDto.PubId != 0)
        {
            if (await _unitOfWork.PublisherRepository.FindByIdAsync(entityDto.PubId.GetValueOrDefault()) is null)
                throw new KeyNotFoundException($"Entity with id {entityDto.PubId} not found");
        }

        var entity = await _unitOfWork.BookRepository.FindByIdAsync(entityDto.BookId);
        if (entity is null) throw new KeyNotFoundException($"Entity with id {entityDto.BookId} not found");
        _mapper.Map(entityDto, entity);
        await _unitOfWork.BookRepository.UpdateAsync(entity);
        await _unitOfWork.CommitAsync();
    }
}
