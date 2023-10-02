﻿namespace EStore.Application.DTOs;

public partial class OrderDto
{
    public int OrderId { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public DateTime? ShippedDate { get; set; }
    public decimal? Freight { get; set; }// money
    public bool IsDeleted { get; set; }

    public int? MemberId { get; set; }
    public virtual ICollection<OrderDetailDto> OrderDetails { get; set; }
}
