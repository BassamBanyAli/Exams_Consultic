using System;
using System.Collections.Generic;

namespace Exams_Consultic.Models;

public partial class Currency
{
    public string CurrencyCode { get; set; } = null!;

    public string CurrencyName { get; set; } = null!;

    public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();
}
