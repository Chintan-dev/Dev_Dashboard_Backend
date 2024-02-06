using System;
using System.Collections.Generic;

namespace Dev_Dashboard.Model;

public partial class UserMenuIdInOrder
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int UserMenuIdInOrder1 { get; set; }

    public virtual UserDetail User { get; set; } = null!;

    public virtual UserMenu UserMenuIdInOrder1Navigation { get; set; } = null!;
}
