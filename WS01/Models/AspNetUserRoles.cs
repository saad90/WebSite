using System;
using System.Collections.Generic;

namespace WS01.Models
{
    public partial class AspNetUserRoles
    {
        public int Pk { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
