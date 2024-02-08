using System;
using System.Collections.Generic;

namespace Dev_Dashboard.Model;

public partial class UserMenu
{
    public int Id { get; set; }

    public string MenuName { get; set; } = null!;

    public string? MenuDescription { get; set; }
    public string? Path { get; set; }

    public virtual ICollection<UserAssignMenu> UserAssignMenus { get; set; } = new List<UserAssignMenu>();

    public virtual ICollection<UserMenuIdInOrder> UserMenuIdInOrders { get; set; } = new List<UserMenuIdInOrder>();
}
