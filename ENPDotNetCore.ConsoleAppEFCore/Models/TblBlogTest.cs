using System;
using System.Collections.Generic;

namespace ENPDotNetCore.ConsoleAppEFCore.Models;

public partial class TblBlogTest
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public string BlogCategory { get; set; } = null!;

    public string BlogStatus { get; set; } = null!;
}
