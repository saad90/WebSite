﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class IxMaterielsTypes
    {
        public IxMaterielsTypes()
        {
            Materiels = new HashSet<Materiels>();
        }

        public int PkIxMaterielsTypes { get; set; }

        [Required(ErrorMessage = "Le champ Type Materiel doit être rempli")]
        [Display(Name = "Type Materiel")]
        public string MaterielType { get; set; }

        public virtual ICollection<Materiels> Materiels { get; set; }
    }
}
