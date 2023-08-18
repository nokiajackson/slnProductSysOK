using System;
using System.Collections.Generic;

namespace prjProductSys.Models;

public partial class TMember
{
    public string Uid { get; set; } = null!;

    public string? Pwd { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }
}
