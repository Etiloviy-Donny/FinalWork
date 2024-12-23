using System;
using System.Collections.Generic;

namespace ServiceLayer.Models;

public partial class OrderProduct
{
    public int? OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public short Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNumberNavigation { get; set; } = null!;
}
