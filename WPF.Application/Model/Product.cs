using System;
using System.Collections.Generic;

namespace WPF.Application.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? Amount { get; set; }
}
