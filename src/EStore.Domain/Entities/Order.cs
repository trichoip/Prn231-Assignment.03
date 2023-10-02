namespace EStore.Domain.Entities;

public partial class Order
{
    public int OrderId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public decimal? Freight { get; set; }
    public bool IsDeleted { get; set; }

    public int? MemberId { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
