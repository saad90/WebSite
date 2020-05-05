using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class LinksInterventions
    {
        public int PkInterventions { get; set; }

        [Display(Name = "Type Intervention")]
        [Required(ErrorMessage = "Please enter Type Intervention")]
        public int FkInterventionsTypes { get; set; }

        [Display(Name = "Email")]
        public string FkAspNetUsers { get; set; }

        [Display(Name = "Antenne")]
        [Required(ErrorMessage = "Please enter Antenne")]
        public int FkIxAntenne { get; set; }

        [Display(Name = "Statut")]
        public int FkMaterielsStatuts { get; set; }

        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string Commentaire { get; set; }

        [Display(Name = "Utilisateur")]
        public virtual AspNetUsers FkAspNetUsersNavigation { get; set; }

        [Display(Name = "Type Intervention")]
        public virtual IxInterventionsTypes FkInterventionsTypesNavigation { get; set; }

        [Display(Name = "Agence")]
        public virtual IxAntenne FkIxAntenneNavigation { get; set; }

        [Display(Name = "Statut")]
        public virtual IxMaterielsStatuts FkMaterielsStatutsNavigation { get; set; }
    }
}
