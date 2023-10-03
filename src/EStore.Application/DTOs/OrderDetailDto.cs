using EStore.Application.Mappings;
using EStore.Domain.Entities;

namespace EStore.Application.DTOs;

public partial class OrderDetailDto : IMapFrom<OrderDetail>
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public double? Discount { get; set; }
}
