using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class Manufacturer
{
    public int MunufacturerId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
