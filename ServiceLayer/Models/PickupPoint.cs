using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class PickupPoint
{
    public int OrderPickupPoint { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
