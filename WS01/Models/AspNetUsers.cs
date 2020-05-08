using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            LinksMaterielsIxMaterielStatuts = new HashSet<LinksMaterielsIxMaterielStatuts>();
            LinksInterventions = new HashSet<LinksInterventions>();
        }

        public string Id { get; set; }
        [Display(Name = "Nom d'utilisateur")]
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        [Display(Name = "Confirmation du mail")]
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        [Display(Name = "Téléphone")]
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
        public virtual ICollection<LinksInterventions> LinksInterventions { get; set; }

    }
}
