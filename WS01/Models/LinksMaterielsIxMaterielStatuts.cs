﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WS01.Models
{
    public partial class LinksMaterielsIxMaterielStatuts
    {
        public int Pk { get; set; }
        [Display(Name = "Materiel")]
        public int? FkMateriels { get; set; }
        [Display(Name = "Statut")]
        public int? FkMaterielsStatuts { get; set; }
        [Display(Name = "Antenne")]
        public int? FkIxAntenne { get; set; }
        [Display(Name = "Email")]
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
