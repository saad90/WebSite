using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class IxMaterielsStatuts
    {
        public IxMaterielsStatuts()
        {
            LinksMaterielsIxMaterielStatuts = new HashSet<LinksMaterielsIxMaterielStatuts>();
        }

        public int PkIxMaterielsStatuts { get; set; }

        [Required(ErrorMessage = "Le champ Statut du Materiel doit être rempli")]
        [Display(Name = "Statut du Materiel")]
        public string MaterielStatut { get; set; }

        public virtual ICollection<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
        public virtual ICollection<LinksInterventions> LinksInterventions { get; set; }

    }
}
