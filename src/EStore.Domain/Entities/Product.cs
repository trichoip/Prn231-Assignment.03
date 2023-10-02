﻿namespace EStore.Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Weight { get; set; }
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public bool IsDeleted { get; set; }

    public int? CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
