using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class Materiels
    {
        public Materiels()
        {
            LinksMaterielsIxMaterielStatuts = new HashSet<LinksMaterielsIxMaterielStatuts>();
        }

        public int PkMateriels { get; set; }

        [Display(Name = "Type Materiel")]
        public int? FkMaterielsTypes { get; set; }

        [Required(ErrorMessage = "Le champ Référence doit être rempli")]
        [Display(Name = "Référence")]
        public string Identifiant { get; set; }

        [Display(Name = "Type Materiel")]
        public virtual IxMaterielsTypes FkMaterielsTypesNavigation { get; set; }

        public virtual ICollection<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
    }
}
