namespace EStore.Application.DTOs;

public partial class OrderDetailDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }//money
    public int Quantity { get; set; }
    public double? Discount { get; set; }

    public virtual OrderDto Order { get; set; }
    public virtual ProductDto Product { get; set; }
}
