using System;
using System.Collections.Generic;

namespace Dev_Dashboard.Model;

public partial class UserAssignMenu
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int MenuId { get; set; }

    public virtual UserMenu Menu { get; set; } = null!;

    public virtual UserDetail User { get; set; } = null!;
}
