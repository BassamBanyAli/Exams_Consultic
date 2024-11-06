using System;
using System.Collections.Generic;

namespace Exams_Consultic.Models;

public partial class PurchaseOrderHeader
{
    public string PurchId { get; set; } = null!;

    public string? Vendor { get; set; }

    public string? CurrencyCode { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Currency? CurrencyCodeNavigation { get; set; }

    public virtual Vendor? VendorNavigation { get; set; }
}
