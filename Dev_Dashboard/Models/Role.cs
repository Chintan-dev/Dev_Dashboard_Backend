using System;
using System.Collections.Generic;

namespace Dev_Dashboard.Model;

public partial class Role
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public bool Active { get; set; }
  
    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
