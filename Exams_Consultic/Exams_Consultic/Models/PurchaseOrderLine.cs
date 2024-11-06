using System;
using System.Collections.Generic;

namespace Exams_Consultic.Models;

public partial class PurchaseOrderLine
{
    public string? PurchId { get; set; }

    public string? ItemId { get; set; }

    public decimal? Qty { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Amount { get; set; }

    public virtual PurchaseOrderHeader? Purch { get; set; }
}
