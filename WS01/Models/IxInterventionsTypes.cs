using System;
using System.Collections.Generic;

namespace WS01.Models
{
    public partial class IxInterventionsTypes
    {
        public IxInterventionsTypes()
        {
            LinksInterventions = new HashSet<LinksInterventions>();
        }

        public int PkIxInterventionsTypes { get; set; }
        public string InterventionType { get; set; }

        public virtual ICollection<LinksInterventions> LinksInterventions { get; set; }
    }
}
