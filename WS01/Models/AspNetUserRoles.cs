using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class AspNetUserRoles
    {
        public int Pk { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string UserId { get; set; }
        [Display(Name = "Nom du rôle")]
        public string RoleId { get; set; }

        [Display(Name = "Nom du rôle")]
        public virtual AspNetRoles Role { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public virtual AspNetUsers User { get; set; }
    }
}
