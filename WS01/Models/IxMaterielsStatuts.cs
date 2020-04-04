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
        [Display(Name = "Statut Materiel")]
        public string MaterielStatut { get; set; }

        public virtual ICollection<LinksMaterielsIxMaterielStatuts> LinksMaterielsIxMaterielStatuts { get; set; }
    }
}
