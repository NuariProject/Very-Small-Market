using System;
using System.Collections.Generic;

namespace User_Management_Service.Context
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsDelete { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
