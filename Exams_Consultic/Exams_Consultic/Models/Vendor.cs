using System;
using System.Collections.Generic;

namespace Exams_Consultic.Models;

public partial class Vendor
{
    public string VendorId { get; set; } = null!;

    public string? VendorName { get; set; }

    public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; } = new List<PurchaseOrderHeader>();
}
