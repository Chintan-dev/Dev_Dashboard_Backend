using System;
using System.Collections.Generic;

namespace Dev_Dashboard.Model;

public partial class UserDetail
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Active { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserAssignMenu> UserAssignMenus { get; set; } = new List<UserAssignMenu>();

    public virtual ICollection<UserMenuIdInOrder> UserMenuIdInOrders { get; set; } = new List<UserMenuIdInOrder>();
}
