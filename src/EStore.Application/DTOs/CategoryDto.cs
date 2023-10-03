using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs;

public partial class CategoryDto : IMapFrom<Category>
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}
