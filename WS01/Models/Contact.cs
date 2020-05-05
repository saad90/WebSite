using System;
using System.Collections.Generic;

namespace WS01.Models
{
    public partial class Contact
    {
        public int PkIxContact { get; set; }
        public string AdresseMail { get; set; }
        public int? PhoneNumber { get; set; }
        public string Objet { get; set; }
        public string Message { get; set; }
    }
}
