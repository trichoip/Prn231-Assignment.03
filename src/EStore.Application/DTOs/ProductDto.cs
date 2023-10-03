using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs;

public partial class ProductDto : IMapFrom<Product>
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public bool IsDeleted { get; set; }

    public int? CategoryId { get; set; }
}
