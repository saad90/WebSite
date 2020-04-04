using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class LinksMaterielsIxMaterielStatuts
    {
        public int Pk { get; set; }
        public int? FkMateriels { get; set; }
        public int? FkMaterielsStatuts { get; set; }
        public int? FkIxAntenne { get; set; }
        public string FkAspNetUsers { get; set; }
        public DateTime? DateDebut { get; set; }
        public DateTime? DateFin { get; set; }
        public string Commentaire { get; set; }

        [Display(Name = "Utilisateur")]
        public virtual AspNetUsers FkAspNetUsersNavigation { get; set; }
        [Display(Name = "Agence")]
        public virtual IxAntenne FkIxAntenneNavigation { get; set; }
        [Display(Name = "Materiel")]
        public virtual Materiels FkMaterielsNavigation { get; set; }
        [Display(Name = "Statut")]
        public virtual IxMaterielsStatuts FkMaterielsStatutsNavigation { get; set; }
    }
}
