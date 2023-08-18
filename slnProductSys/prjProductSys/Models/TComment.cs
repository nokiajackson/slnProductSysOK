using System;
using System.Collections.Generic;

namespace prjProductSys.Models;

public partial class TComment
{
    public int Id { get; set; }

    public string? Comment { get; set; }

    public DateTime? PublishDate { get; set; }

    public string? ReComment { get; set; }

    public string? IsReComment { get; set; }

    public string? ProductId { get; set; }
}
