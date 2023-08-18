using System;
using System.Collections.Generic;

namespace prjProductSys.Models;

public partial class TProduct
{
    public string ProductId { get; set; } = null!;

    public string? ProductName { get; set; }

    public int? Price { get; set; }

    public string? Img { get; set; }

    public int? CategoryId { get; set; }
}
