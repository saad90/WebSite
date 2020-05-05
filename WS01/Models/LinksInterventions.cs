using System;
using System.Collections.Generic;

namespace WS01.Models
{
    public partial class LinksInterventions
    {
        public int PkInterventions { get; set; }
        public int FkInterventionsTypes { get; set; }
        public string FkAspNetUsers { get; set; }
        public int FkIxAntenne { get; set; }
        public int FkMaterielsStatuts { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string Commentaire { get; set; }

        public virtual AspNetUsers FkAspNetUsersNavigation { get; set; }
        public virtual IxInterventionsTypes FkInterventionsTypesNavigation { get; set; }
        public virtual IxAntenne FkIxAntenneNavigation { get; set; }
        public virtual IxMaterielsStatuts FkMaterielsStatutsNavigation { get; set; }
    }
}
