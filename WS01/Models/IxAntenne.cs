using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class IxAntenne
    {
        public IxAntenne()
        {
            LinksMaterielsIxMaterielStatuts = new HashSet<LinksMaterielsIxMaterielStatuts>();
        }
        public int PkAntenne { get; set; }

        [Required(ErrorMessage = "Le champ Ville doit être rempli")]
        [Display(Name = "Ville")]
        public string Ville { get; set; }

        [Required(ErrorMessage = "Le champ Code Postal doit être rempli")]
        [Display(Name = "Code Postal")]
        public string CodePostal { get; set; }

        [Required(ErrorMessage = "Le champ Téléphone doit être rempli")]
        [Display(Name = "Téléphone")]
        public string Tel { get; set; }

        public virtual ICollection<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
        public virtual ICollection<LinksInterventions> LinksInterventions { get; set; }

    }
}
