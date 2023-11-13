﻿using System;
using System.Collections.Generic;

namespace LINQLab.Models;

public partial class ShoppingCartItem
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
